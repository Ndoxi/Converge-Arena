This project is a compact gameplay prototype focused on clean architecture, extensibility, and maintainability rather than graphical quality.  
All visuals are intentionally simple and provided through modular decorators.

---

## How to Run the Project

- The project should normally be started from the **Bootstrap** scene.  
- **However**, if you accidentally launch *any other scene*, an editor-only helper script will automatically load the Bootstrap scene first.  
- For testing in the Unity Editor, the player can also be controlled using:
  - **WASD** — movement  
  - **J** — attack  

(Mobile controls via the on-screen joystick and attack button remain fully functional.)

---

## Architecture Overview

### **Entities**
- All units (player and AI) use **the same `Entity` class** and **the same internal state machine**.
- Behavioral differences come from their assigned **Command Center**:
  - `PlayerCommandCenter` — handles player input.
  - `AIBrainCommandCenter` — autonomous AI logic and decisions.

### **Decorators**
- Visual representation (material, team colors, highlights, etc.) is controlled by **Entity Decorators**.
- Decorators initialize visuals during entity creation and update them dynamically (e.g., when team changes).

### **Configuration**
- `EntityConfigurator` handles:
  - Stat initialization
  - Team assignment
  - Providing the appropriate command center
  - Attaching and initializing decorators

---

## AI Behavior

Bots demonstrate the following behaviors:
- Maintaining **loose group cohesion** via group-center influence.
- Moving between **randomized waypoints**.
- Detecting enemies nearby and engaging them.
- Operating through a state machine driven by their AI Command Center.

A dedicated **EntityGroupSystem** manages bot groups:
- Update managed groups
- Selects new waypoint if the current one iss reached 
- Allows unified movement patterns

---

## Gameplay Scene Context

The **GameplayContext** is a scene-specific dependency container created **only on the Gameplay scene**.  
It exists exclusively for gameplay purposes and is destroyed when the scene unloads.

It registers all **scene-level services**, such as:

- `EntityGroupSystem`  
- `TeamConversionSystem`  
- `MatchStateService`  
- `WorldPointsService`  
- other gameplay-only dependencies

This keeps global services clean and ensures gameplay systems exist only during gameplay.

**Important:**  
The GameplayContext **always initializes first**, because its **Script Execution Order** is explicitly set earlier than Unity’s default.  
This guarantees that all gameplay services are ready before any gameplay objects begin executing.

---

## Notes

- The architecture is modular and designed for rapid iteration.
- Systems communicate through abstractions and interfaces.
- The prototype emphasizes maintainability, clarity, and ease of extension over visual detail.
