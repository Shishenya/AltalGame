using UnityEngine;

public class TriggerFadeItems : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        FadeItem[] fadeItems = collision.gameObject.GetComponentsInChildren<FadeItem>();

        if (fadeItems.Length > 0)
        {
            for (int i = 0; i < fadeItems.Length; i++)
            {
                fadeItems[i].FadeOutItem();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        FadeItem[] fadeItems = collision.gameObject.GetComponentsInChildren<FadeItem>();

        if (fadeItems.Length > 0)
        {
            for (int i = 0; i < fadeItems.Length; i++)
            {
                fadeItems[i].FadeInItem();
            }
        }

    }
}
