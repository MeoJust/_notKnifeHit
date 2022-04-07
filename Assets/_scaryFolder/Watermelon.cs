using UnityEngine;

public class Watermelon : MonoBehaviour
{
    [SerializeField] GameObject[] _watermelonCuts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "knife")
        {
            FindObjectOfType<MusicPlayer>().PlaySFX(FindObjectOfType<MusicPlayer>().MelonSFX);
            transform.parent = null;

            if (FindObjectOfType<Knife>() != null)
            {
                Instantiate(FindObjectOfType<Knife>().GetMelonPart(), transform);
            }

            foreach (var cut in _watermelonCuts)
            {
                Rigidbody2D cutRb = cut.GetComponent<Rigidbody2D>();
                cutRb.AddForce(new Vector2(Random.Range(-100f, 100f), Random.Range(-200f, 200f)));
                cutRb.gravityScale = 1f;
            }

            GetComponent<CircleCollider2D>().enabled = false;
            FindObjectOfType<ScoreKeeper>().IncreaceMelons();
        }
    }
}
