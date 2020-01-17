using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using TMPro;
namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;

        public GameObject mensajeDerrota;
        public GameObject vidasRestantes;
        public GameObject balaPrefab;
        public GameObject ammoPrefab;
        public float timetoSpawn;
        public AudioClip cannonShoot;
        public AudioClip linternaClick;
        public AudioClip cambiarArma;
        public GameObject linterna;
        public GameObject SAB1;
        public GameObject pistola;
        public GameObject lugarBala;
        public GameObject shotLocation;
        private int armaEquipadaEspecial;
        private float velCaminar;
        private float velCorrer;
        private float alturaSalto;
        private float recargaSAB1=0f;
        private float recargaPistola = 0f;
        private int armaEquipada;
        private int armas;
        private int vidas;
        private float tempRecuperacion;
        private bool derechoEquipamiento;
        public Animator animGun;
        public Animator animCannon;
        public Animator animPJ;
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);

            velCaminar = m_WalkSpeed;
            velCorrer = m_RunSpeed;
            alturaSalto = m_JumpSpeed;
            SAB1.SetActive(false);
            armaEquipadaEspecial=0;
            armas = 0;
            armaEquipada = 0;
            derechoEquipamiento = false;
            vidas = 3;
            tempRecuperacion = 30f;
        }


        // Update is called once per frame
        private void Update()
        {
            vidasRestantes.GetComponent<TextMeshProUGUI>().text = "VIDAS: " + vidas;
            if ((tempRecuperacion <= 0)&&(vidas < 3) && (vidas > 0))
            {
                Invoke("Restauracion", 1.5f);
                vidasRestantes.GetComponent<Animator>().SetTrigger("avisoRecuperacion");
                tempRecuperacion = 30f;
            }

            if (vidas <= 0)
            {
                vidas = 0;
                mensajeDerrota.GetComponent<TextMeshProUGUI>().text = "¡HAS CAIDO!";
                mensajeDerrota.GetComponent<Animator>().SetTrigger("mensaje");
            }
            //Activar/Desactivar linterna
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_AudioSource.clip = linternaClick;
                m_AudioSource.Play();

                if (linterna.activeSelf == false) linterna.SetActive(true);
                else linterna.SetActive(false);
            }

            if ((armaEquipada == 0)&&(armaEquipadaEspecial==0))
            {
                pistola.SetActive(true);
                if (Input.GetMouseButtonDown(0) && (recargaPistola <= 0f))
                {
                    recargaPistola = 0.5f;
                    RaycastHit hit;
                    Invoke("MultipleShoot", 0);
                    
                    if (Physics.Raycast(shotLocation.transform.position,shotLocation.transform.TransformDirection(Vector3.forward),out hit, Mathf.Infinity, ~0))
                    {
                        if (hit.collider.tag=="Enemigos")
                        {

                                hit.collider.GetComponent<ZombieMechanics>().AddDamage(3);
                        }
                    }

                }
            }
            else pistola.SetActive(false);



            //Cambiar armas principales
            if (armaEquipadaEspecial == 0)
            {

                m_WalkSpeed = velCaminar;
                m_RunSpeed = velCorrer;
                m_JumpSpeed = alturaSalto;
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (armaEquipada < armas - 1)
                    {
                        armaEquipada++;
                    }
                    else
                    {
                        armaEquipada = 0;

                    }
                }

                if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (armaEquipada <= 0)
                    {
                        armaEquipada = armas - 1;
                    }
                    else
                    {
                        armaEquipada--;
                    }

                }

            }


            //Activar/desactiva cañón
            if (Input.GetKeyDown(KeyCode.Tab)&& derechoEquipamiento)
            {
                m_AudioSource.clip = cambiarArma;
                m_AudioSource.Play();

                if (SAB1.activeSelf == false)
                {
                    armaEquipadaEspecial = 1;
                    SAB1.SetActive(true);
                }

                else
                {
                    armaEquipadaEspecial = 0;
                    SAB1.SetActive(false);
                }
            }

            //Si lleva el cañón se limita su movimiento
            if (armaEquipadaEspecial == 1)
            {
                m_WalkSpeed = 3;
                m_RunSpeed = 6;
                m_JumpSpeed = 2.2f;

                if (Input.GetMouseButtonDown(0) && (recargaSAB1 <= 0f))
                {
                    recargaSAB1 = 3f;

                    GameObject bala = Instantiate(balaPrefab, lugarBala.transform.position, lugarBala.transform.rotation);

                    //m_AudioSource.clip = cannonShoot;
                    //m_AudioSource.Play();
                    //Invoke("shoot", timetoSpawn);
                    SAB1.transform.position -= Vector3.forward;
                    animCannon.SetTrigger("disparando");
                    animPJ.SetTrigger("disparando");
                }

            }



            recargaPistola -= Time.deltaTime;
            recargaSAB1 -= Time.deltaTime;

            if (vidas < 3) tempRecuperacion -= Time.deltaTime;
            
            

            #region UpdateControl
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
            #endregion


        }

        public void Shoot()
        {
            GameObject ammo = Instantiate(ammoPrefab, shotLocation.transform.position, shotLocation.transform.rotation);
            
        }

        public void DañarJugador()
        {
            vidasRestantes.GetComponent<Animator>().SetTrigger("avisoGolpe");
            vidas--;
        }

        public void Restauracion()
        {
            vidas++;
        }
        public void MultipleShoot()
        {
            animGun.SetTrigger("disparando");
            animPJ.SetTrigger("disparando");
            Invoke("Shoot", 0f);
            Invoke("Shoot", 0.02f);
            Invoke("Shoot", 0.05f);
        }

       public void SetDerecho()
        {
            derechoEquipamiento = true;
        }
        
        #region FPSController
        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
    #endregion
}
