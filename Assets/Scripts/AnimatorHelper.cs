using System;
using System.Collections;
using System.Collections.Generic;
using Spessman.Inventory.Extensions;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Spessman.Engine.AnimationHelper
{
    public class AnimatorHelper : MonoBehaviour
    {
        public Animator animator;

        public AvatarMask[] avatarMask;

        public AnimationClip[] animationClip = new AnimationClip[2];
        public AnimationPlayableOutput[] output = new AnimationPlayableOutput[2];
        public AnimationLayerMixerPlayable[] mixer = new AnimationLayerMixerPlayable[2];

        public Hands hands;

        private PlayableGraph[] graph = new PlayableGraph[2];

        public AnimationClip emptyClip;

        private void Start()
        {
            hands.HandContainers[0].ItemAttached += delegate { UpdateHandsAnimation(); };
            hands.HandContainers[1].ItemAttached += delegate { UpdateHandsAnimation(); };

            hands.HandContainers[0].ItemDetached += delegate { UpdateHandsAnimation(); };
            hands.HandContainers[1].ItemDetached += delegate { UpdateHandsAnimation(); };
            SetupPlayable();

            for (int i = 0; i < 2; i++)
            {
                animationClip[i] = emptyClip;
            }
        }

        private void SetupPlayable()
        {
            for (int i = 0; i < 2; i++)
            {
                graph[i] = PlayableGraph.Create();
                graph[i].SetTimeUpdateMode(DirectorUpdateMode.GameTime);

                mixer[i] = AnimationLayerMixerPlayable.Create(graph[i], 1);
                mixer[i].SetLayerMaskFromAvatarMask(0, avatarMask[i]);

                output[i] = AnimationPlayableOutput.Create(graph[i], "Hands" + i, animator);
                output[i].SetSourcePlayable(mixer[i]);

                var emptyClip = AnimationClipPlayable.Create(graph[i], animationClip[i]);
                mixer[i].ConnectInput(0, emptyClip, 0, 0);

            }
        }

        private void UpdateHandsAnimation()
        {
            // Gets the items animations from the hands
            AnimationClip[] newAnimationClips = hands.ItemsAnimationClips;

            // saves the current clips in this array
            AnimationClip[] currentClips = animationClip;
            // and then updates it to the new ones
            animationClip = newAnimationClips;

            // for each hand
            for (int i = 0; i < 2; i++)
            {
                // if there's a null clip we set it to the empty clip
                // this is due AnimationClipPlayables can't be null. Ever
                if (newAnimationClips[i] == null)
                    newAnimationClips[i] = emptyClip;

                // same thing
                if (animationClip[i] == null)
                    animationClip[i] = emptyClip;

                // create the ClipPlayable
                var clip = AnimationClipPlayable.Create(graph[i], animationClip[i]);

                // Update the input
                //mixer[i].DisconnectInput(0);

                // connect the clip to an input
                mixer[i].ConnectInput(0, clip, 0, 0);

                //mixer[i].GetInput(0).SetAnimatedProperties(animationClip[i]);

                // if the clip is empty clip we just lerp to 0
                if (newAnimationClips[i] == emptyClip)
                {
                    var emptyClip = AnimationClipPlayable.Create(graph[i], animationClip[i]);
                    //mixer[i].DisconnectInput(0);
                    StartCoroutine(LerpAvatarWeight(i, 0));
                }
                else
                    StartCoroutine(LerpAvatarWeight(i, 1));

                if (!graph[i].IsPlaying())
                    graph[i].Play();
            }
        }

        // Lerps the avatar weights
        public IEnumerator LerpAvatarWeight(int index, float value)
        {
            float weight = mixer[index].GetInputWeight(0);
            while (weight != value)
            {
                weight = Mathf.Lerp(weight, value, Time.deltaTime * 5);
                mixer[index].SetInputWeight(0, weight);

                yield return new WaitForSeconds(0);
            }
        }
    }
}