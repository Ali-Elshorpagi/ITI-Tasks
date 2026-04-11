# ajax_json

## Overview
This exercise demonstrates how to call a remote API with jQuery AJAX and render the response into a table.

## What It Does
- Sends a `GET` request to `https://dummyjson.com/posts`
- Reads the returned `posts` array from JSON data
- Dynamically creates table rows and cells using jQuery
- Appends values such as `id`, `title`, `body`, `tags`, `reactions.likes`, and `views`

## Concepts Covered
- `$(document).ready()` lifecycle usage
- `$.ajax()` with success callback
- Dynamic element creation with `$('<tr>')` and `$('<td>')`
- Rendering arrays (`tags`) into readable text

## Files
- `index.html`: table structure and script loading
- `script.js`: AJAX request and row rendering logic

## Expected Outcome
When the page loads, posts are fetched from the API and displayed as table rows without reloading the page.
