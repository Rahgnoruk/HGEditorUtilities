using UnityEditor;
using UnityEngine;
//Unity UI Extensions License (BSD)
//Copyright(c) 2019

//Redistribution and use in source and binary forms, with or without modification,
//are permitted provided that the following conditions are met:

//1.Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

//2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer 
//   in the documentation and/or other materials provided with the distribution.

//3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived 
//   from this software without specific prior written permission.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
//INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY,
//OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
//OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
//OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
//EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
public class uGUITools : MonoBehaviour {
	[MenuItem("uGUI/Anchors to Corners %[")]
	static void AnchorsToCorners(){
		foreach(Transform transform in Selection.transforms){
			RectTransform t = transform as RectTransform;
			RectTransform pt = Selection.activeTransform.parent as RectTransform;

			if(t == null || pt == null) return;
			
			Vector2 newAnchorsMin = new Vector2(t.anchorMin.x + t.offsetMin.x / pt.rect.width,
			                                    t.anchorMin.y + t.offsetMin.y / pt.rect.height);
			Vector2 newAnchorsMax = new Vector2(t.anchorMax.x + t.offsetMax.x / pt.rect.width,
			                                    t.anchorMax.y + t.offsetMax.y / pt.rect.height);

			t.anchorMin = newAnchorsMin;
			t.anchorMax = newAnchorsMax;
			t.offsetMin = t.offsetMax = new Vector2(0, 0);
		}
	}

	[MenuItem("uGUI/Corners to Anchors %]")]
	static void CornersToAnchors(){
		foreach(Transform transform in Selection.transforms){
			RectTransform t = transform as RectTransform;

			if(t == null) return;

			t.offsetMin = t.offsetMax = new Vector2(0, 0);
		}
	}

	[MenuItem("uGUI/Mirror Horizontally Around Anchors %;")]
	static void MirrorHorizontallyAnchors(){
		MirrorHorizontally(false);
	}

	[MenuItem("uGUI/Mirror Horizontally Around Parent Center %:")]
	static void MirrorHorizontallyParent(){
		MirrorHorizontally(true);
	}

	static void MirrorHorizontally(bool mirrorAnchors){
		foreach(Transform transform in Selection.transforms){
			RectTransform t = transform as RectTransform;
			RectTransform pt = Selection.activeTransform.parent as RectTransform;
			
			if(t == null || pt == null) return;

			if(mirrorAnchors){
				Vector2 oldAnchorMin = t.anchorMin;
				t.anchorMin = new Vector2(1 - t.anchorMax.x, t.anchorMin.y);
				t.anchorMax = new Vector2(1 - oldAnchorMin.x, t.anchorMax.y);
			}

			Vector2 oldOffsetMin = t.offsetMin;
			t.offsetMin = new Vector2(-t.offsetMax.x, t.offsetMin.y);
			t.offsetMax = new Vector2(-oldOffsetMin.x, t.offsetMax.y);

			t.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);
		}
	}

	[MenuItem("uGUI/Mirror Vertically Around Anchors %'")]
	static void MirrorVerticallyAnchors(){
		MirrorVertically(false);
	}
	
	[MenuItem("uGUI/Mirror Vertically Around Parent Center %\"")]
	static void MirrorVerticallyParent(){
		MirrorVertically(true);
	}
	
	static void MirrorVertically(bool mirrorAnchors){
		foreach(Transform transform in Selection.transforms){
			RectTransform t = transform as RectTransform;
			RectTransform pt = Selection.activeTransform.parent as RectTransform;
			
			if(t == null || pt == null) return;
			
			if(mirrorAnchors){
				Vector2 oldAnchorMin = t.anchorMin;
				t.anchorMin = new Vector2(t.anchorMin.x, 1 - t.anchorMax.y);
				t.anchorMax = new Vector2(t.anchorMax.x, 1 - oldAnchorMin.y);
			}
			
			Vector2 oldOffsetMin = t.offsetMin;
			t.offsetMin = new Vector2(t.offsetMin.x, -t.offsetMax.y);
			t.offsetMax = new Vector2(t.offsetMax.x, -oldOffsetMin.y);
			
			t.localScale = new Vector3(t.localScale.x, -t.localScale.y, t.localScale.z);
		}
	}
}