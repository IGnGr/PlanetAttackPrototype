﻿using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

namespace qASIC.Options.Menu
{
    [AddComponentMenu("qASIC/Options/Slider")]
    public class OptionsSlider : MenuOption, IPointerUpHandler
    {
        private Slider slider;

        public bool round = true;
        [Tooltip("Round value multiplier")]
        public double roundValue = 0.1;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            if (slider != null) slider.onValueChanged.AddListener((float value) => SetValueSlider(value, false));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (slider == null) return;
            SetValueSlider(slider.value, true);
        }

        private void SetValueSlider(float value, bool log)
        {
            if (slider.wholeNumbers && int.TryParse(value.ToString(), out int intResult))
            {
                SetValue(intResult, log);
                return;
            }
            SetValue(round ? (float)(Mathf.Round(value / (float)roundValue) * roundValue) : value, log);
        }

        public override string GetLabel()
        {
            if (slider == null) return string.Empty;
            return $"{optionLabelName}{(round ? Mathf.Round(slider.value / (float)roundValue) * roundValue : slider.value)}";
        }

        public override void LoadOption()
        {
            if (slider == null) return;
            if (!OptionsController.TryGetOptionValue(optionName, out string optionValue) || 
                !float.TryParse(optionValue, out float value)) return;
            slider.SetValueWithoutNotify(value);
        }
    }
}