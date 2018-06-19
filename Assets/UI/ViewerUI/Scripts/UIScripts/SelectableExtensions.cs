using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI
{
    static public class SelectableExtensions
    {
        public delegate bool FilterDelegate(Selectable a, Selectable b);

        private static Vector3 GetPointOnRectEdge(RectTransform rect, Vector2 dir)
        {
            if (rect == null)
                return Vector3.zero;
            if (dir != Vector2.zero)
                dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
            dir = rect.rect.center + Vector2.Scale(rect.rect.size, dir * 0.5f);
            return dir;
        }

        public static Selectable FindSelectablePerpendicular(this Selectable selectable, Vector3 direction, FilterDelegate filter)
        {
            direction = direction.normalized;
            Vector3 localDir = selectable.transform.rotation * direction;
            Vector3 pos = selectable.transform.TransformPoint(GetPointOnRectEdge(selectable.transform as RectTransform, localDir));

            float maxScore = Mathf.Infinity;
            float maxDistance = Mathf.Infinity;
            Selectable bestPick = null;
            Ray ray = new Ray(pos, Quaternion.AngleAxis(90, selectable.transform.forward) * localDir); //Rotate the localDir 90° around the selectable's forward axis, to get a direction along selectable.rect side

            for (int i = 0; i < Selectable.allSelectables.Count; i++)
            {
                Selectable sel = Selectable.allSelectables[i];

                if (sel == selectable || sel == null)
                    continue;
                if (!sel.IsInteractable() || sel.navigation.mode == Navigation.Mode.None)
                    continue;
                if (filter(selectable, sel) == false)
                    continue;

                var selRect = sel.transform as RectTransform;
                Vector3 selCenter = selRect != null ? sel.transform.TransformPoint((Vector3)selRect.rect.center) : Vector3.zero;
                Vector3 myVector = selCenter - pos;
                // Value that is the distance out along the direction.
                float dot = Vector3.Dot(localDir, myVector.normalized);
                // Skip elements that are in the wrong direction or which have zero distance.
                if (dot <= 0)
                    continue;

                float score = Vector3.Cross(ray.direction, selCenter - ray.origin).sqrMagnitude; //Returns the distance between the selected rect's center and a ray along the edge of selectable

                if (score < maxScore)
                {
                    maxScore = score;
                    maxDistance = myVector.sqrMagnitude;
                    bestPick = sel;
                }
                else if (score == maxScore)
                {
                    if (maxDistance < myVector.sqrMagnitude)
                        continue;
                    else
                    {
                        maxScore = score;
                        maxDistance = myVector.sqrMagnitude;
                        bestPick = sel;
                    }
                }
            }
            return bestPick;
        }

        public static Selectable FindSelectable(this Selectable selectable, Vector3 direction, FilterDelegate filter)
        {
            direction = direction.normalized;
            Vector3 localDir = Quaternion.Inverse(selectable.transform.rotation) * direction;
            Vector3 pos = selectable.transform.TransformPoint(GetPointOnRectEdge(selectable.transform as RectTransform, localDir));
            float maxScore = Mathf.NegativeInfinity;
            Selectable bestPick = null;

            for (int i = 0; i < Selectable.allSelectables.Count; i++)
            {
                Selectable sel = Selectable.allSelectables[i];

                if (sel == selectable || sel == null)
                    continue;
                if (!sel.IsInteractable() || sel.navigation.mode == Navigation.Mode.None)
                    continue;
                if (filter(selectable, sel) == false)
                    continue;

                var selRect = sel.transform as RectTransform;
                Vector3 selCenter = selRect != null ? (Vector3)selRect.rect.center : Vector3.zero;
                Vector3 myVector = sel.transform.TransformPoint(selCenter) - pos;
                // Value that is the distance out along the direction.
                float dot = Vector3.Dot(direction, myVector.normalized);
                // Skip elements that are in the wrong direction or which have zero distance.
                // This also ensures that the scoring formula below will not have a division by zero error.
                if (dot <= 0)
                    continue;

                // This scoring function has two priorities:
                // - Score higher for positions that are closer.
                // - Score higher for positions that are located in the right direction.
                // This scoring function combines both of these criteria.
                // It can be seen as this:
                //   Dot (dir, myVector.normalized) / myVector.magnitude
                // The first part equals 1 if the direction of myVector is the same as dir, and 0 if it's orthogonal.
                // The second part scores lower the greater the distance is by dividing by the distance.
                // The formula below is equivalent but more optimized.
                //
                // If a given score is chosen, the positions that evaluate to that score will form a circle
                // that touches pos and whose center is located along dir. A way to visualize the resulting functionality is this:
                // From the position pos, blow up a circular balloon so it grows in the direction of dir.
                // The first Selectable whose center the circular balloon touches is the one that's chosen.
                float score = dot / myVector.sqrMagnitude;

                if (score > maxScore)
                {
                    maxScore = score;
                    bestPick = sel;
                }
            }
            return bestPick;
        }

        static public Selectable FindSelectableOnLeft(this Selectable selectable, FilterDelegate filter)
        {
            return selectable.FindSelectable(Vector3.left, filter);
        }

        static public Selectable FindSelectableOnRight(this Selectable selectable, FilterDelegate filter)
        {
            return selectable.FindSelectable(Vector3.right, filter);
        }

        static public Selectable FindSelectableOnUp(this Selectable selectable, FilterDelegate filter)
        {
            return selectable.FindSelectable(Vector3.up, filter);
        }

        static public Selectable FindSelectableOnDown(this Selectable selectable, FilterDelegate filter)
        {
            return selectable.FindSelectable(Vector3.down, filter);
        }

        static public Selectable FindSelectableOnLeftPerpendicular(this Selectable selectable, FilterDelegate filter)
        {
            return selectable.FindSelectablePerpendicular( Vector3.left, filter);
        }

        static public Selectable FindSelectableOnRightPerpendicular(this Selectable selectable, FilterDelegate filter)
        {
            return selectable.FindSelectablePerpendicular( Vector3.right, filter);
        }

        static public Selectable FindSelectableOnUpPerpendicular(this Selectable selectable, FilterDelegate filter)
        {
            return selectable.FindSelectablePerpendicular( Vector3.up, filter);
        }

        static public Selectable FindSelectableOnDownPerpendicular(this Selectable selectable, FilterDelegate filter)
        {
            return selectable.FindSelectablePerpendicular( Vector3.down, filter);
        }
    }
}
