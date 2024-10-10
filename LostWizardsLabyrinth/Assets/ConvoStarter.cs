using DialogueEditor;
using UnityEngine;

public class ConvoStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConvo;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                ConversationManager.Instance.StartConversation(myConvo);
            }
        }
    }
}
 