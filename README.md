# EnvironmentQuerySystem-Unity
System for querying and obtaining the highest-utility point in an environment in unity.
Similar to the Unreal Environment Query System, this system will create a user-selected formation of query points that will perform environment queries in which each query point is evaluated on a series of user-defined query evaluations and the highest utility point is returned.

# How to use
The main tool of the Environment Query System is the Querier class. This class is responsible for performing a raycast from the query point location (with an offset) and either hitting acceptable ground points (Ground Layer) or unaccepable/obstacle points (Reject Layer). It then performs a series of evaluations to select the highest utility query point that hit an acceptable layer.
There are two examples of queriers in the project:
GroundQuerier class: Performs a downward (Vector3.down) raycast. This script can be added to a player or enemy character.
SphereQuerier class: Performs a spherical raycast toward the center of the querying object. For example, if the object was a small meteor in space, this script would be added to the meteor game-object.
In this script, you must include:
- Formation Builder: The formation or shape in which the query points will be laid out in space. There are basic formations included in the project, such as Grid (NxN grid shaped formation), Fibonacci Disc (formation of points that are uniformly distant from each other), Donut (rings of N points forming a donut-shape), Fibonacci Sphere (a 3-dimensional sphere of points that are equidistant from each other on the sphere). The algorithms for constructing these formations are in the FormationBuilders static class, this class can be extended if you want to have formations currently unavailable.
- Query Evaluations: The evaluations that will be used to select the highest-utility query point. These can be anything that makes a query point more/less desirable, such as line of sight to a target, distance from self or target, overlapping specific game objects or object-types, etc.
- Ground Layer/Reject Layer: These are acceptable/unacceptable layers for the raycast, acceptable layers will maintain a utility of 1 (no change) and unacceptable layers will receive a utility of zero (disqualified).
