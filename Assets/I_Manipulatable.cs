using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Manipulatable{

	//remember position is in WORLD SPACE, not LOCAL space.
	void OnManipulationStart (Vector3 position);

	void OnManipulationUpdate (Vector3 position);

	void OnManipulationComplete (Vector3 position);

	void OnManipulationCancel ();
}
