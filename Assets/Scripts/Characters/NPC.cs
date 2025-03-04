using UnityEngine;

namespace PokemonGame.Characters
{
    public class NPC : Character, IInteract
    {
        [SerializeField, TextArea(5, 5)] private string dialog;

        public string Dialogs => dialog;

        public override void Interact(Transform sender)
        {
            GetComponent<CharacterController>().LookAt(-sender.GetComponent<CharacterController>().FacingDirection);

            GetComponent<CharacterController>().enabled = false;
            sender.GetComponent<CharacterController>().enabled = false;

            DialogBox.Instance.DisplayText(dialog);

            if (DialogBox.Instance.IsDialogOver)
            {
                GetComponent<CharacterController>().enabled = true;
                sender.GetComponent<CharacterController>().enabled = true;
            }
        }
    }
}
