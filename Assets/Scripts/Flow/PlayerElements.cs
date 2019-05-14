using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Input;
using VoxelPanda.Player.Presentation;

public class PlayerElements : MonoBehaviour {
	public Transform playerTransform;
    public PhysicsController physicsController;
	public RawAccInput accInput;
    public RawTouchInput touchInput;
	public AnimationManager animationManager;
	public ArrowUI arrowUI;
	public CamBehaviour camBehaviour;
	public Particles particles;
	public SFX sfx;
	public StaminaUI staminaUI;
	public TouchDragUI touchDragUI;
    public FlingCalculator flingCalculator;
    public CurveCalculator curveCalculator;
    public TiltArrowUI tiltArrowUI;
    public GameObject movementUIComponents;
}
