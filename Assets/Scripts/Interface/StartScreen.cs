using System;
using System.Linq;
using Assets.Scripts.Common;
using Assets.Scripts.Common.Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Interface
{
    public class StartScreen : BaseInterface
	{
        public static StartScreen Instance;

        public void Awake()
		{
			Instance = this;
        }

        public void Start()
        {

        }

        protected override void OnOpen()
        {

        }
    }
}