# Lab01

## Overview

This lab delves into the powerful programmatic APIs introduced natively in **HTML5**. It covers extracting hardware location data, rendering dynamic shapes on a bitmap surface, and manipulating raw media elements via JavaScript.

---

## Files

- **`Lab01.pdf`**: The official lab problem sheet containing all instructions and requirements.

```text
Lab01/
├── Lab01.pdf
├── Task 01/
│   └── index.html
├── Task 02/
│   ├── index.html
│   ├── scripts.js
│   └── styles.css
└── Task 03/
    ├── assets/
    ├── index.html
    ├── scripts.js
    └── styles.css
```

---

## Tasks / Features

### 1. Geolocation & Maps (Task 01)
- **Geolocation API**: Uses `navigator.geolocation.getCurrentPosition` to dynamically retrieve the hardware's current latitude and longitude.
- **Google Maps API**: Integrates an external API dynamically appending a Google Map script, passing the retrieved coordinates to map the user context using the newer `importLibrary("maps")` patterns.

### 2. Canvas Rendering (Task 02)
- **Canvas API**: Defines a drawing area using the HTML5 `<canvas>` element.
- **Dynamic Drawing**: Pairs an HTML color picker input (`<input type="color">`) with a drawing script to actively render circular nodes directly onto the canvas buffer.

### 3. Custom Audio Player (Task 03)
- **Media API**: Implements a customized wrapper around the invisible `<audio>` element.
- **Playlists & Controls**: Intercepts standard browser playbacks by mapping custom buttons (`playAudio`, `pauseAudio`, `stopAudio`) to the core API functions `.play()` and `.pause()`.
- **Seekbar**: Uses an `<input type="range">` combined with JavaScript to manipulate the audio track's current time logically.

---

## Key Concepts Demonstrated

- **Native Hardware Access** — Safely tapping into the device's GPS parameters natively from the browser.
- **Canvas Context** — Manipulating pixels via `getContext('2d')` for arbitrary shape renders.
- **Media Manipulation** — Bypassing default browser media appearances and writing bespoke UI behaviors manipulating track loading and state.

---
