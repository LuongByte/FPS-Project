using UnityEditor;

[CustomEditor(typeof(Interactable), true)]

public class InteractableEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        //Currently selected game object
        Interactable interactable = (Interactable)target;
        if(target.GetType() == typeof(EventOnlyInteractable)){
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            //EditorGUILayout.HelpBox("Can only use Unity Events.", MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>() == null){
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>(); 
            }
        }
        else{
            base.OnInspectorGUI();
            //Checks if object uses events and if it doesn't contain a component then adds one
            if(interactable.useEvents){
                if(interactable.GetComponent<InteractionEvent>() == null){
                interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }
            //If object does not use events but still has the component then remove it
            else{
                if(interactable.GetComponent<InteractionEvent>() != null){
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
                }
            }
        }
    }
}
