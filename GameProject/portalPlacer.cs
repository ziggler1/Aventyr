﻿using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xna = Microsoft.Xna.Framework;

namespace Game
{
    public class PortalPlacer
    {
        public const float PortalMargin = 0.02f;
        private const float RayCastMargin = 0.0001f;
        public static FixtureIntersection Raycast(Scene scene, Line ray)
        {
            Vector2 rayBegin = ray.Vertices[0];
            Vector2 rayEnd = ray.Vertices[1];
            if (rayBegin != rayEnd)
            {
                List<FixtureIntersection> intersections = new List<FixtureIntersection>();
                IntersectPoint intersectLast = new IntersectPoint();
                scene.PhysWorld.RayCast(
                    delegate(Fixture fixture, Xna.Vector2 point, Xna.Vector2 normal, float fraction)
                    {
                        Vector2 rayIntersect = VectorExt2.ConvertTo(point);
                        rayIntersect = rayIntersect + (rayIntersect - rayBegin).Normalized() * RayCastMargin;
                        switch (fixture.Shape.ShapeType)
                        {
                            case ShapeType.Polygon:
                                {
                                    PolygonShape shape = (PolygonShape)fixture.Shape;
                                    Vector2[] vertices = VectorExt2.ConvertTo(shape.Vertices);
                                    var transform = new FarseerPhysics.Common.Transform();
                                    fixture.Body.GetTransform(out transform);
                                    Matrix4 matTransform = MatrixExt4.ConvertTo(transform);
                                    vertices = VectorExt2.Transform(vertices, matTransform);
                                    for (int i = 0; i < vertices.Count(); i++)
                                    {
                                        IntersectPoint intersect = MathExt.LineIntersection(
                                            vertices[i],
                                            vertices[(i + 1) % vertices.Count()],
                                            rayBegin,
                                            rayIntersect,
                                            true);
                                        if (intersect.Exists)
                                        {
                                            if (fixture.UserData != null)
                                            {
                                                FixtureUserData userData = (FixtureUserData)fixture.UserData;
                                                if (userData.EdgeIsExterior[i] == false)
                                                {
                                                    break;
                                                }
                                            }
                                            intersectLast = intersect;
                                            intersections.Add(new FixtureIntersection(fixture, i, (float)intersect.T));
                                            break;
                                        }
                                        Debug.Assert(i + 1 < vertices.Count(), "Intersection edge was not found in shape.");
                                    }
                                    break;
                                }
                            case ShapeType.Circle:
                                {
                                    break;
                                }
                        }
                        return fraction;
                    },
                    VectorExt2.ConvertToXna(rayBegin),
                    VectorExt2.ConvertToXna(rayEnd));
                IOrderedEnumerable<FixtureIntersection> sortedIntersections = intersections.OrderBy(item => (rayBegin - item.GetPosition()).Length);
                if (sortedIntersections.Count() > 0)
                {
                    return sortedIntersections.ToArray()[0];
                }
            }
            return null;
        }

        /// <summary>
        /// Given a intersection point, this returns a valid intersection point (which could be the same position), or null if none exists.
        /// </summary>
        /// <param name="intersection"></param>
        /// <param name="portal"></param>
        /// <returns></returns>
        public static FixtureIntersection GetValid(FixtureIntersection intersection, Portal portal)
        {
            Line portalLine = new Line(portal.GetWorldVerts());
            float portalSize = portalLine.Length;
            Line edge = intersection.GetWorldEdge();
            float portalSizeT = portalSize / edge.Length + PortalMargin * 2;
            if (portalSizeT > 1)
            {
                return null;
            }
            float portalT = intersection.EdgeT;
            portalT = Math.Max(portalT, portalSizeT / 2);
            portalT = Math.Min(portalT, 1 - portalSizeT / 2);
            FixtureIntersection intersectValid = new FixtureIntersection(intersection.Fixture, intersection.EdgeIndex, portalT);
            return intersectValid;
        }
    }
}