// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using System;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// NAVIGATION MANAGER
	// ===================================================================================
    public class NavigationManager : MonoBehaviour {
    
        public event Action<Vector3> PlayerMoved;
        public event Action<Vector3> PlayerTurned;
        
        [Range(1,10)] public float turningSpeed;
        [Range(1,10)] public float movementSpeed;
        public float baseHeightOffset;
        
        public DirectionType facingDirection { get; set; }
                
        protected DateTime lastInput;
        protected bool forwardFlag;
        protected bool backFlag;
        protected bool leftFlag;
        protected bool rightFlag;
        protected bool waitForKeyUp;
        protected bool waitForButtonUp;
		protected Rigidbody rb3D;
		protected bool _changedDirection;
		
		// -------------------------------------------------------------------------------
		// Start
		// -------------------------------------------------------------------------------
        private void Start() {
        
        	rb3D = GetComponent <Rigidbody> ();
            lastInput = DateTime.Now;
            
            if (Finder.settings != null)
                Finder.settings.Loaded += settings_Loaded;
        }
        
		// -------------------------------------------------------------------------------
		// SmoothMovement
		// -------------------------------------------------------------------------------
        protected IEnumerator SmoothMovement (Vector3 end) {
			
			enabled = false;
        	lastInput = DateTime.Now;
        	
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            
            while(sqrRemainingDistance > float.Epsilon) {
                Vector3 newPostion = Vector3.MoveTowards(rb3D.position, end, movementSpeed * Time.deltaTime);
                rb3D.MovePosition (newPostion);
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;
                Finder.map.UpdateInteractableDirection();
                yield return null;
            }
            
            OnMoved();
            
        }
        
 		// -------------------------------------------------------------------------------
		// SmoothTurn
		// -------------------------------------------------------------------------------
        protected IEnumerator SmoothTurn(float degrees) {
        	
        	enabled = false;
        	DateTime lastInput = DateTime.Now;
        	float remainingDegrees = Mathf.Abs(degrees);
        	float countDegrees;
        	
        	countDegrees = turningSpeed;
        	if (degrees < 0) countDegrees *= -1;
        	
        	while(remainingDegrees > 0) {
        		transform.Rotate(Vector3.up, countDegrees);
        		remainingDegrees -= turningSpeed;
        		Finder.map.UpdateInteractableDirection();
        		yield return null;
        	}
        	
        	transform.forward = transform.forward.Round(0);
        	
            OnTurned();
        	
        }
        
		// -------------------------------------------------------------------------------
		// TurnRight
		// -------------------------------------------------------------------------------
        public void TurnRight() {
            if (!enabled) return;
            StartCoroutine(SmoothTurn(90)); 
        }
        
		// -------------------------------------------------------------------------------
		// TurnLeft
		// -------------------------------------------------------------------------------
        public void TurnLeft() {
            if (!enabled) return;
            StartCoroutine(SmoothTurn(-90));
        }

		// -------------------------------------------------------------------------------
		// MoveForward
		// -------------------------------------------------------------------------------
        public void MoveForward() {
            if (!enabled) return;

            if (CheckDirection(transform.forward)) {
            	Vector3 newPosition = transform.position + transform.forward;
            	
            	StartCoroutine (SmoothMovement(newPosition));
            } else {
            	Finder.audio.PlaySFX(SFX.WallBump);
            }

        }
        
		// -------------------------------------------------------------------------------
		// MoveLeft
		// -------------------------------------------------------------------------------
        public void MoveLeft() {
            if (!enabled) return;

			if (CheckDirection(-transform.right)) {
				Vector3 newPosition = transform.position - transform.right;
				
				StartCoroutine (SmoothMovement(newPosition));
			} else {
				Finder.audio.PlaySFX(SFX.WallBump);
			}

        }
        
		// -------------------------------------------------------------------------------
		// MoveRight
		// -------------------------------------------------------------------------------
        public void MoveRight() {
            if (!enabled) return;

            if (CheckDirection(transform.right)) {
            	Vector3 newPosition = transform.position + transform.right;
            	
                StartCoroutine (SmoothMovement(newPosition));
            } else {
                Finder.audio.PlaySFX(SFX.WallBump);
            }

        }
        
		// -------------------------------------------------------------------------------
		// MoveBackward
		// -------------------------------------------------------------------------------
        public void MoveBackward() {
            if (!enabled) return;
     
			if (CheckDirection(-transform.forward)) {
				Vector3 newPosition = transform.position - transform.forward;
				
				StartCoroutine (SmoothMovement(newPosition));
			} else {
				Finder.audio.PlaySFX(SFX.WallBump);
			}

        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void NormalizeTransform() {
            transform.position = transform.position.RoundLaterals(0);
            transform.forward = transform.forward.Round(0);
        }
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void OnMoved() {
            Finder.audio.PlaySFX(SFX.Footstep);
            NormalizeTransform();
            Finder.map.UpdateInteractableDirection();
            Finder.party.MapExplorationInfo.AddExploredSpace(Finder.map.MapName, new Vector2(Finder.party.transform.position.x, Finder.party.transform.position.z));
            facingDirection = Vector3Helper.CardinalDirection(transform.forward);
            if (!Finder.battle.InBattle) enabled = true;
            changedDirection = false;
            OnPlayerMoved();
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void OnTurned() {
            facingDirection = Vector3Helper.CardinalDirection(transform.forward);
            if (!Finder.battle.InBattle) enabled = true;
            changedDirection = true;
            OnPlayerTurned();
        }
        
		// -------------------------------------------------------------------------------
		// OnPlayerTurned (Event)
		// -------------------------------------------------------------------------------
        private void OnPlayerTurned() {
            Action<Vector3> handler = PlayerTurned;
            if (handler != null)
                handler(Finder.party.transform.forward);
        }
        
		// -------------------------------------------------------------------------------
		// OnPlayerMoved (Event)
		// -------------------------------------------------------------------------------
        private void OnPlayerMoved() {
            Action<Vector3> handler = PlayerMoved;
            if (handler != null)
                handler(Finder.party.transform.position);
        }

		// -------------------------------------------------------------------------------
		// CheckDirection
		// -------------------------------------------------------------------------------
        private bool CheckDirection(Vector3 direction) {
            return !Physics.Raycast(transform.position, direction, 1, 1 << LayerMask.NameToLayer("wall"));
        }
        		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        void settings_Loaded(object sender, EventArgs e) {
            Finder.settings.Loaded -= settings_Loaded;
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void Update() {
        	
        	if (!enabled) return;
        	        	
            bool wasKeyboardUsed = CheckKeyboardInput();
            
            if (!wasKeyboardUsed) {
                if (forwardFlag) {
                    if (!waitForButtonUp)
                        MoveForward();
                } else if (leftFlag) {
                    if (!waitForButtonUp)
                        MoveLeft();
                } else if (rightFlag) {
                    if (!waitForButtonUp)
                        MoveRight();
                } else if (backFlag) {
                    if (!waitForButtonUp)
                        MoveBackward();
                } else {
                    waitForButtonUp = false;
                }
            }

        }

		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private bool CheckKeyboardInput() {
        	
        	if (!enabled) return false;
        	
            if (Input.GetKey(KeyCode.W)) {
                if (!waitForKeyUp) {
                    MoveForward();
                    return true;
                }
            }  else if (Input.GetKey(KeyCode.A)) {
                if (!waitForKeyUp) {
                    MoveLeft();
                    return true;
                }
            } else if (Input.GetKey(KeyCode.D)) {
                if (!waitForKeyUp) {
                    MoveRight();
                    return true;
                }
            } else if (Input.GetKey(KeyCode.S)) {
                if (!waitForKeyUp) {
                    MoveBackward();
                    return true;
                }
            } else if (Input.GetKeyUp(KeyCode.Q)) {
                if (!waitForKeyUp) {
                    TurnLeft();
                    return true;
                }
            } else if (Input.GetKeyUp(KeyCode.E)) {
                if (!waitForKeyUp) {
                    TurnRight();
                    return true;
                }
                
            } else if (Input.GetKeyUp(KeyCode.M)) {    
                ToggleMinimap();
                
            } else if (Input.GetKeyUp(KeyCode.I)) {    
                ToggleInventory();
                
            } else {
                waitForKeyUp = false;
            }
            
            return false;
            
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected void ToggleMinimap() {
        
        	if (Finder.map.locationType == LocationType.Dungeon) {
				if (Finder.ui.MapOpened) {
					Finder.ui.PushState(UIState.MinimapHide);
				} else {
					Finder.ui.PushState(UIState.MinimapShow);
				}
       		}
        
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected void ToggleInventory() {
        
			if (Finder.ui.currentState == UIState.Inventory) {
				Finder.ui.PopState();
			} else {
				Finder.ui.PushState(UIState.Inventory);
			}
       		
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void SetForwardFlag(bool val) {
            forwardFlag = val;
        }

        public void SetLeftFlag(bool val) {
            leftFlag = val;
        }

        public void SetRightFlag(bool val)  {
            rightFlag = val;
        }

        public void SetBackFlag(bool val) {
            backFlag = val;
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void ResetFlags() {
            backFlag 		= false;
            forwardFlag 	= false;
            leftFlag 		= false;
            rightFlag 		= false;
            waitForKeyUp 	= true;
            waitForButtonUp = true;
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public bool changedDirection {
        	get {
        		bool dir = _changedDirection;
        		_changedDirection = false;
        		return dir;
        	}
        	set {
        		_changedDirection = value;
        	}
        }
        
        // -------------------------------------------------------------------------------
        
    }
}