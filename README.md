# Waypoint-Tactics
Instructions for demo:
Create a character that follows a predetermined set of waypoints around a map with walls and/or other obstacles. Populate the map with another set of “cover” waypoints. When the player is detected have the character use the cover waypoints to fire. Once the player is no longer in view, the character will return to its path.

This demo uses raycasts to determine visibility and assigns a cover value for each cover point in the level.
