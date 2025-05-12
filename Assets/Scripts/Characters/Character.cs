using UnityEngine;

namespace PokemonGame.Characters
{
    public class Character : MonoBehaviour
    {
        private const float CENTER_OFFSET = 0.5f;
        private const float RAYCAST_DISTANCE = 1f;
        private const float OFFSET_Y = 0.5f;

        private RaycastHit2D[] hit;

        [SerializeField] protected string characterName;

        public string CharacterName => characterName;
        public int Money { get; set; }
        public string ID { get; set; }


        private void Start()
        {
            // Make sure the position are rounded values before offsetting the character.
            transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), 0);

            // Offset the character to the middle center of the tile in x axis.    
            transform.position -= new Vector3(CENTER_OFFSET, 0, 0);

            IDGenerator generator = new IDGenerator(10000, 99999);
            ID = generator.GetID();
        }

        private void Awake()
        {
           // controller = GetComponent<CharacterAnimator>();
        }

        private void Update()
        {
            //Interaction();
        }

        public void Interaction()
        {
            Vector3 rayCastOrigin = new Vector3(transform.position.x, transform.position.y + OFFSET_Y, transform.position.z);

            //hit = Physics2D.RaycastAll(rayCastOrigin, new Vector2(controller.FacingDirection.x, controller.FacingDirection.y), RAYCAST_DISTANCE);

            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i])
                {
                    if (Input.GetKeyDown(KeyBind.Accept))
                    {
                        IInteract[] interect = hit[i].collider.gameObject.GetComponents<IInteract>();

                        foreach (IInteract t in interect)
                        {
                            t.OnInteract(this);
                        }
                    }
                }
            }

        }
    }
}
