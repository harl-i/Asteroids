using Game.Core.UI;
using Game.Infrastructure.UI;
using TMPro;
using UnityEngine;
using Zenject;

public class ShipHudView : MonoBehaviour
{
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

        _coordinatesText.text = $"Coordinates: {viewModel.Position.x:F1}, {viewModel.Position.y:F1}";
        _rotationText.text = $"Angle: {viewModel.RotationDeg:F1}";
        _speedText.text = $"Speed: {viewModel.Speed:F1}";
        _healthText.text = $"HP: {viewModel.CurrentHealth}/{viewModel.MaxHealth}";
        _laserChargesText.text = $"Laser Charges: {viewModel.LaserCharges}/{viewModel.LaserMaxCharges}";
        _laserCooldownText.text = $"Laser Cooldown: {viewModel.LaserCooldownRemaining:F1}";
    }
}
