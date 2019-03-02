using System.Collections.Generic;
using Scripts;
using UnityEngine;
using Utility;

namespace Managers
{
    public class LaundryManager : Subject
    {
        public Sprite[] laundrySprites;
        public Laundry[] laundryInLevel;


        public List<Laundry> activeLaundry = new List<Laundry>();

        public int CollectedLaundry;
        public int initialLaundry;

        private void Start()
        {
            initialLaundry = laundryInLevel.Length;
            for (int i = 0; i < laundryInLevel.Length; i++)
            {
                activeLaundry.Add(laundryInLevel[i]);
                int sp = Random.Range(0, laundrySprites.Length);
                laundryInLevel[i].Initialize(laundrySprites[sp]);
                laundryInLevel[i].transform.SetParent(this.transform);
            }
        }

        private void Update()
        {
            foreach (var laundry in activeLaundry)
            {
                laundry.Animate();
            }
        }

        public void UpdateLaundryCount(Laundry laundry)
        {
            CollectedLaundry++;
            for (int i = 0; i < activeLaundry.Count; i++)
            {
                if (laundry == activeLaundry[i])
                {
                    activeLaundry.RemoveAt(i);
                }
            }
        }

        public void NotifyStart()
        {
            //calls on notify in a timely fashion
            Notify(Event.COLLECT_COIN);
        }
    }
}