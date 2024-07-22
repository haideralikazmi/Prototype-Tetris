using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using HAK.Core;

namespace HAK.Gameplay.PhoneCharge
{
    public class Phone : MonoBehaviour
    {
        [SerializeField] private Image _batteryFillImage;
        [SerializeField] private TextMeshProUGUI _batteryHealthText;

        public void Initialize()
        {
            _batteryHealthText.color = Constants.PhoneCharging.BatteryEmptyColorCode;
            _batteryFillImage.color =  Constants.PhoneCharging.BatteryEmptyColorCode;
            _batteryFillImage.fillAmount = Configs.ViewConfig.BatteryEmptyTargetFillValue;
        }
        
        public void ChargeBattery()
        {
            var targetValue = Configs.ViewConfig.BatteryFullTargetFillValue;
            AnimateBattery(targetValue, SetBatteryFullState);
        }

        public void DrainBattery()
        {
            var targetValue = Configs.ViewConfig.BatteryEmptyTargetFillValue;
            AnimateBattery(targetValue, SetBatteryEmptyState);
        }

        private void AnimateBattery(float targetHealth, Action callback)
        {
            var batteryHealth = _batteryFillImage.fillAmount;
            var duration = Configs.ViewConfig.ChargingAnimationDuration;

            DOVirtual.Float(batteryHealth, targetHealth, duration, (batteryHealth) =>
                {
                    _batteryFillImage.fillAmount = batteryHealth;
                })
                .SetEase(Ease.InOutQuart)
                .OnComplete(() =>
                {
                   callback?.Invoke();
                });
        }
        
        private void SetBatteryFullState()
        {
            var batteryFull = Constants.PhoneCharging.BatteryFull;
            var color = Constants.PhoneCharging.BatteryFullColorCode;
            _batteryFillImage.color = color;
            _batteryHealthText.color = color;
            _batteryHealthText.text = batteryFull;
        }
        
        private void SetBatteryEmptyState()
        {
            var batteryFull = Constants.PhoneCharging.BatteryEmpty;
            var color = Constants.PhoneCharging.BatteryEmptyColorCode;
            _batteryFillImage.color = color;
            _batteryHealthText.color = color;
            _batteryHealthText.text = batteryFull;
        }
    }
}