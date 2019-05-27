using UnityEngine;
using UnityEngine.UI;

namespace NightAtTheRijksmuseum
{
    public class Player : Character
    {

        [SerializeField]
        private Image attackRadial;

        [SerializeField]
        private Image damageImage;

        [SerializeField]
        private float damgeImageFadeTime;

        private float timeElapsedSinceDamage;

        [SerializeField]
        private float rotateSpeed;

        private bool damaged;

        // Update is called once per frame
        private void Update()
        {
            ProcessInput();
            currentDelay += Time.deltaTime;
            attackRadial.fillAmount = Mathf.Min(currentDelay / attackDelay, 1.0f);

            if(damaged)
            {
                if(timeElapsedSinceDamage >= damgeImageFadeTime)
                {
                    timeElapsedSinceDamage = 0;
                    damaged = false;
                }
                else
                {
                    timeElapsedSinceDamage += Time.deltaTime;
                    Color imageColor = damageImage.color;
                    imageColor.a = Mathf.Min(timeElapsedSinceDamage / damgeImageFadeTime, 1.0f);
                    damageImage.color = imageColor;
                }
            }
            else
            {
                if(damageImage.color.a > 0)
                {
                    timeElapsedSinceDamage += Time.deltaTime;
                    Color imageColor = damageImage.color;
                    imageColor.a = 1 - Mathf.Min(timeElapsedSinceDamage / damgeImageFadeTime, 1.0f);
                    damageImage.color = imageColor;
                }
                else
                {
                    timeElapsedSinceDamage = 0;
                }
            }
        }

        private void ProcessInput()
        {
            ProcessMomementInput();
            ProcessMouseInput();
        }

        private void ProcessMomementInput()
        {
            //Process keyboard input.
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (input != Vector3.zero)
            {
                Move(input);
            }

            //Process mouse movement input.
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            transform.Rotate(0, horizontal, 0);
        }

        private void ProcessMouseInput()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(targetEnemy != null)
                { 
                    if (currentDelay >= attackDelay)
                    {
                        Attack();
                    }
                }
            }
        }

        protected void Move(Vector3 input)
        {
            //Calculate movement.
            Vector3 movement = input * movementSpeed;

            //Movement is relative to the camera.
            Vector3 relativeMovement = Camera.main.transform.TransformVector(movement);

            //We want no movement on the y axis.
            relativeMovement.y = 0;

            //Move.
            controller.Move(relativeMovement * Time.deltaTime);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if(targetEnemy == null)
            { 
                if(other.GetComponent<Enemy>())
                {
                    targetEnemy = other.GetComponent<Enemy>();
                    other.GetComponent<Enemy>().OnTargeted();
                }
            }
        }

        protected override void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Enemy>())
            {
                other.GetComponent<Enemy>().OnLostTargeting();
                targetEnemy = null;
            }
        }

        public override void IsAttacked(Vector3 direction)
        {
            base.IsAttacked(direction);
            damaged = true;
        }
    }
}