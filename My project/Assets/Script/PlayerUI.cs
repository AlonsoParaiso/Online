using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using TMPro;
using Photon.Pun;

namespace Com.MyCompany.MyGame
{
    public class PlayerUI : MonoBehaviourPun
    {

        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private TextMeshProUGUI playerNameText;

        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;
        [SerializeField]


        private PlayerManager target;




        void Start()
        {
            target = GetComponentInParent<PlayerManager>();
            playerNameText.text = photonView.Owner.NickName;
            

        }
       
        void Update()
        {
            playerHealthSlider.value = target.Health;
        }






    }
}
