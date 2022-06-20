using UnityEngine;

public class EquipmentMeshSetter : MonoBehaviour
{
    [SerializeField] private PlayerEquipment _playerEquipment;
    [SerializeField] private EquipmentMesh[] _equipmentMeshes;

    private void OnEnable()
    {
        _playerEquipment.EquipmentChanged += OnEquipmentChanged;
    }

    private void OnDisable()
    {
        _playerEquipment.EquipmentChanged -= OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        int slotIndex = (int)newItem.EquipmentSlot;

        switch (newItem.Name)
        {
            case "Sword":
                _equipmentMeshes[slotIndex].MeshRenderers[0].enabled = false;
                _equipmentMeshes[slotIndex].MeshRenderers[1].enabled = true;
                break;
            case "Bat":
                _equipmentMeshes[slotIndex].MeshRenderers[1].enabled = false;
                _equipmentMeshes[slotIndex].MeshRenderers[0].enabled = true;
                break;
            case "Armor":
                _equipmentMeshes[slotIndex].MeshRenderers[1].enabled = false;
                _equipmentMeshes[slotIndex].MeshRenderers[0].enabled = true;
                break;
            case "Vest":
                _equipmentMeshes[slotIndex].MeshRenderers[0].enabled = false;
                _equipmentMeshes[slotIndex].MeshRenderers[1].enabled = true;
                break;
            case "Hat":
                _equipmentMeshes[slotIndex].MeshRenderers[0].enabled = true;
                _equipmentMeshes[slotIndex].MeshRenderers[1].enabled = false;
                break;
            case "Helmet":
                _equipmentMeshes[slotIndex].MeshRenderers[0].enabled = false;
                _equipmentMeshes[slotIndex].MeshRenderers[1].enabled = true;
                break;
            default:
                break;
        }
    }
}