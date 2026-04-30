using Fusion;
using TMPro;
using UnityEngine;

namespace FusionDemo {
    /// <summary>
    /// Class responsible for displaying the player username on top of the avatar.
    /// </summary>
    public class PlayerUsername : NetworkBehaviour {
        // Networked string to store the player username.
        [Networked] private NetworkString<_32> username { get; set; }

        [SerializeField] private TMP_Text usernameText;
        [SerializeField] private PlayerColor playerColor;

        /// <summary>
        /// Set the player username.
        /// </summary>
        public void SetUsernameLabel(string username) {
            this.username = username;
        }

        public string GetUsername() {
            return usernameText.text;
        }

        private void OnEnable() {
            playerColor.OnColorChanged += ReactToColorChange;
        }

        private void OnDisable() {
            playerColor.OnColorChanged -= ReactToColorChange;
        }

        public override void Spawned() {
            usernameText.SetText(username.ToString());
        }

        // Set the username text based on the player active color value.
        private void ReactToColorChange(Color color) {
            usernameText.color = color;
        }
    }
}