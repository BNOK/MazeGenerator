# 🧩 Eller's Algorithm Maze Generator – Unity C#

A Unity-based implementation of **Eller’s Algorithm** to generate perfect mazes efficiently in real-time using C#. Great for use in procedural level generation, AI pathfinding challenges, or learning algorithmic design in games.

[![Unity Version](https://img.shields.io/badge/unity-2021.3%2B-black.svg)](https://unity.com) [![License: MIT](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

---

## 📌 Features

- ✅ Implements **Eller's Algorithm** for perfect maze generation
- 🧠 Optimized row-by-row maze construction
- 🎮 Real-time visualization inside Unity
- 🛠️ Easily adjustable maze size and speed
- 🎨 Customizable tile prefabs (walls, floors, decorations)

---

## 🧪 How It Works

Eller’s algorithm generates a maze **one row at a time**, ensuring:

- Each cell is part of a unique set
- Horizontal passages are randomly merged
- Vertical connections ensure set propagation
- Final row merges all remaining sets

Result: a fully-connected maze with **no loops and no inaccessible cells**.

🔗 [Learn more about Eller's Algorithm](https://en.wikipedia.org/wiki/Maze_generation_algorithm#Eller's_algorithm)

---
