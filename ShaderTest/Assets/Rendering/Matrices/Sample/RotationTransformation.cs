using UnityEngine;

public class RotationTransformation : Transformation {

	public Vector3 rotation;

	public override Vector3 Apply (Vector3 point) {
		float radZ = rotation.z * Mathf.Deg2Rad;
		float sinZ = Mathf.Sin(radZ);
		float cosZ = Mathf.Cos(radZ);

		point = new Vector3(
			cosZ * point.x - sinZ * point.y,
			sinZ * point.x + cosZ * point.y,
			point.z
		);

		return point;
	}
}