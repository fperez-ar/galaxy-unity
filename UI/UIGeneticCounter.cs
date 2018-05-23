﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGeneticCounter : UICounterBase, IPointerClickHandler {

	public override void reset () {
		base.reset ();
		if (refObj != null) {
			EvHandler.ExecuteEv (GameEvent.RM_GENE_MAT, refObj);
		}
	}

	public virtual void OnPointerClick(PointerEventData eventData) {
		EvHandler.ExecuteEv (GameEvent.RM_GENE_MAT, refObj);
		reset ();
	}
}
