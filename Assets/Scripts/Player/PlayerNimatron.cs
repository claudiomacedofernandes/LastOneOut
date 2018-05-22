using UnityEngine;

namespace LastOneOut
{
    public class PlayerNimatron : IPlayer
    {
        public event OnItemSelectedEvent OnItemSelected = null;

        public void StartTurn()
        {
            Debug.Log("Nimatron StartTurn");
        }

        public void EndTurn()
        {
            Debug.Log("Nimatron EndTurn");
        }
    }
}
