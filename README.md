# Pong

A classic 2D Pong game built in Unity. You take on an AI opponent in a fast-paced match where the ball speeds up with every paddle hit and the angle of each return depends on where the ball strikes your paddle.

**[▶ Play it on itch.io]([ITCH_IO_URL])**

![Gameplay screenshot](docs/screenshot.png)

## How to Play

You control the **right** paddle. The AI controls the left one. Get the ball past the AI to score, and don't let it get past you.

| Action | Keys |
|---|---|
| Move paddle up / down | `W` / `S` or `↑` / `↓` |
| Play again after a match | `R` |

The first player to reach the winning score takes the match. When it ends, press `R` to start over.

*Desktop and keyboard only.*

## Features

- A 3-2-1 countdown before every serve
- The ball speeds up with each paddle hit, up to a cap
- Return angle depends on where the ball hits the paddle, so you can aim
- The ball and its trail show how fast the rally has become
- Sound effects for paddle hits and wall bounces, plus looping background music
- Win and lose screens with instant restart

## Built With

- [Unity](https://unity.com/) (C#)
- Unity Input System package

## Running the Project Locally

1. Install [Git LFS](https://git-lfs.com/) **before** cloning. The audio files are stored with it.
2. Clone the repo:
   ```
   git clone https://github.com/LloydCode597/Pong-Game.git
   ```
3. Open the folder in Unity Hub (Unity version: `[UNITY_VERSION]`).
4. Open `Assets/Scenes/Game` and press Play.

## Credits

**Music:** "TRACK: Pong" by Author: Yesme, Source: [OpenGameArt.org](https://opengameart.org/content/pong-0).

**Sound effects:** [SFX_CREDIT, e.g. "Created with jsfxr (sfxr.me)" or "Pack name by Creator, license, link"].

Thank you to the creators who share their work.

## License

[Code license, e.g. MIT. Add a LICENSE file to match, or delete this section.] Third-party audio remains under its original license, as noted above.
