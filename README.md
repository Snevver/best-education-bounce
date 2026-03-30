## Technische stack
Unity 6.3, C#, 2D orthographic camera. TextMeshPro voor UI.

## Scenestructuur
**MainMenu**:titelmenu, highscore, last score, game over panel, congrats bericht bij nieuw record.
**Game**: de speelbare scene met alle gameplay objecten.

## Scripts & objecten
| Script | GameObject | Functie |
|---|---|---|
| `GameManager.cs` | GameManager | Score, highscore, LastScore, PlayerPrefs, GameOver. Reset highscore met **R**. |
| `PlayerController.cs` | Player | Beweging, bounce, stomp op monster, dood door monster/val, SFX, explosie |
| `CameraFollow.cs` | Main Camera | Volgt speler omhoog, score op basis van camerahoogte |
| `PlatformSpawner.cs` | PlatformSpawner | Spawnt platforms + monsters, gat groter naarmate score stijgt |
| `BoostSpawner.cs` | BoostSpawner | Spawnt boost pickups willekeurig boven het scherm |
| `MonsterController.cs` | Monster prefab | Kijkt richting speler, bounced op platforms op halve kracht |
| `Platform.cs` | Platform prefab | One-way bounce voor speler en monster |
| `Boost.cs` | Boost prefab | Float animatie, lanceert speler 3× hoger, geeft 250 score |
| `FadeOut.cs` / `FadeIn.cs` | FadeOut / FadeIn | Fade naar zwart bij dood, fade terug bij scene load |
| `UIManager.cs` | UIManager | Score, highscore, last score, congrats tekst, death panel |

## Prefabs
Vier prefabs in `Assets/Prefabs`: **Platform**, **Boost**, **Player**, **Monster**.
Platform en Monster hebben een Platform Effector 2D voor one-way collision.