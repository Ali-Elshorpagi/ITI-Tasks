# slider

## Overview
This exercise builds a simple image slider/lightbox using jQuery events and effects.

## What It Does
- Shows a set of thumbnail images
- Opens a centered overlay when a thumbnail is clicked
- Displays the selected image in the slider view
- Navigates through images with next and previous arrows
- Closes the overlay when clicking outside the slider frame

## Concepts Covered
- Element indexing with `images.index(this)`
- Dynamic attribute updates with `.attr('src', ...)`
- Modal-style behavior using class toggling and `fadeIn`/`fadeOut`
- Event bubbling control with `stopPropagation()`

## Files
- `index.html`: thumbnails and slider frame markup
- `style.css`: layout and overlay styling
- `script.js`: selection, navigation, and close behavior

## Expected Outcome
Users can preview images in an interactive overlay and move between images smoothly.
