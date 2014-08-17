using UnityEngine;

namespace swantiez.unity.tools.camera
{
    public static class CameraUtils
    {
        public static bool GetWorldPointFromScreenAtPosZ(Vector3 screenPoint, float worldPosZ, out Vector3 worldPoint)
        {
            worldPoint = Vector3.zero;
            Ray screenPointRay = Camera.main.ScreenPointToRay(screenPoint);

            Vector3 zPoint = new Vector3(0, 0, worldPosZ);
            Plane zPlane = new Plane(-Vector3.forward, zPoint);

            //Debug.Log("CameraUtils::GetWorldPointFromScreenAtPosZ - screenPoint=" + screenPoint + " - zPoint=" + zPoint);

            float rayDistance = 0;
            if (zPlane.Raycast(screenPointRay, out rayDistance))
            {
                worldPoint = screenPointRay.GetPoint(rayDistance);
                //Debug.Log("CameraUtils::GetWorldPointFromScreenAtPosZ - rayDistance=" + rayDistance);
                return true;
            }

            return false;
        }

        public static void GetCameraLimitPointsAtZ(float camZ, out Vector3 camMinPoint, out Vector3 camMaxPoint)
        {
            GetWorldPointFromScreenAtPosZ(new Vector3(0, 0, 0), camZ, out camMinPoint);
            GetWorldPointFromScreenAtPosZ(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0), camZ, out camMaxPoint);
        }

        public static void ComputeCameraSpaceSize(float camZ, out float camSpaceWidth, out float camSpaceHeight)
        {
            Vector3 camMinPoint;
            Vector3 camMaxPoint;
            GetCameraLimitPointsAtZ(camZ, out camMinPoint, out camMaxPoint);
            camSpaceWidth = camMaxPoint.x - camMinPoint.x;
            camSpaceHeight = camMaxPoint.y - camMinPoint.y;
            //Debug.Log("CameraFilterPicture => camSpaceWidth = " + camSpaceWidth + " - camSpaceHeight = " + camSpaceHeight);
        }

        public static void ResizeSprite(SpriteRenderer spriteRenderer, ref Vector3 scale, float newWidth, float newHeight, bool uniformScaling)
        {
            float originalSpriteWidthInUnits = spriteRenderer.sprite.bounds.size.x;
            float originalSpriteHeightInUnits = spriteRenderer.sprite.bounds.size.y;
            float currentSpriteWidthInUnits = originalSpriteWidthInUnits * scale.x;
            float currentSpriteHeightInUnits = originalSpriteHeightInUnits * scale.y;
            float scaleWidthFactor = newWidth / currentSpriteWidthInUnits;
            float scaleHeightFactor = newHeight / currentSpriteHeightInUnits;
            //Debug.Log("CameraFilterPicture - Sprite position = " + spriteRenderer.transform.position + " - original width = " + originalSpriteWidthInUnits + ", height = " + originalSpriteHeightInUnits + " - current width = " + currentSpriteWidthInUnits + ", height = " + currentSpriteHeightInUnits);
            //Debug.Log("CameraFilterPicture => scale factor for width = " + scaleWidthFactor + ", height = " + scaleHeightFactor);

            if (uniformScaling)
            {
                float maxScaleFactor = Mathf.Max(scaleWidthFactor, scaleHeightFactor);
                scaleWidthFactor = maxScaleFactor;
                scaleWidthFactor = maxScaleFactor;
            }

            scale.x *= scaleWidthFactor;
            //scale.y *= scaleHeightFactor; TODO: not correct for height, to adjust
        }
    }
}