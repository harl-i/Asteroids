using Game.Core.UI;
using Game.Infrastructure.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Presentation.UI
{
    public class ShipHudView : MonoBehaviour
    {
        private const string CoordinatesFormat = "Coordinates: {0:F1}, {1:F1}";
        private const string AngleFormat = "Angle: {0:F1}";
        private const string SpeedFormat = "Speed: {0:F1}";
        private const string HealthFormat = "HP: {0}/{1}";
        private const string LaserChargesFormat = "Laser Charges: {0}/{1}";
        private const string LaserCooldownFormat = "Laser Cooldown: {0:F1}";

        [SerializeField] private TextMeshProUGUI _coordinatesText;
        [SerializeField] private TextMeshProUGUI _rotationText;
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _laserChargesText;
        [SerializeField] private TextMeshProUGUI _laserCooldownText;

        private ShipHudPresenter _shipHudPresenter;

        [Inject]
        public void Construct(ShipHudPresenter presenter)
        {
            _shipHudPresenter = presenter;
        }

        private void Update()
        {
            ShipHudViewModel viewModel = _shipHudPresenter.ShipHudViewModel;

            _coordinatesText.text = string.Format(CoordinatesFormat, viewModel.Position.x, viewModel.Position.y);
            _rotationText.text = string.Format(AngleFormat, viewModel.RotationDeg);
            _speedText.text = string.Format(SpeedFormat, viewModel.Speed);
            _healthText.text = string.Format(HealthFormat, viewModel.CurrentHealth, viewModel.MaxHealth);
            _laserChargesText.text = string.Format(LaserChargesFormat, viewModel.LaserCharges, viewModel.LaserMaxCharges);
            _laserCooldownText.text = string.Format(LaserCooldownFormat, viewModel.LaserCooldownRemaining);
        }
    }
}
