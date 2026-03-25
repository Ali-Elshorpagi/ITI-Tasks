"use strict";

const express = require("express");
const { quickSortIterative, quickSortRecursive } = require("../src/quicksort");

const app = express();

app.use(express.json({ limit: "1mb" }));

function parseSortRequest(body) {
    if (body == null || typeof body !== "object") {
        throw new TypeError("Request body must be a JSON object.");
    }

    const {
        array,
        mode = "iterative",
        pivotStrategy = "median3",
        smallPartitionThreshold = 16,
    } = body;

    if (!Array.isArray(array)) {
        throw new TypeError("'array' is required and must be an array.");
    }

    if (array.length > 200000) {
        throw new RangeError("Array is too large. Maximum supported length is 200000.");
    }

    for (let i = 0; i < array.length; i += 1) {
        if (!Number.isFinite(array[i])) {
            throw new TypeError(`array[${i}] must be a finite number.`);
        }
    }

    if (mode !== "iterative" && mode !== "recursive") {
        throw new TypeError("'mode' must be either 'iterative' or 'recursive'.");
    }

    return {
        array,
        mode,
        options: {
            pivotStrategy,
            smallPartitionThreshold,
            inPlace: false,
        },
    };
}

app.get("/api/health", (_req, res) => {
    res.status(200).json({
        status: "ok",
        service: "quicksort-api",
    });
});

app.post("/api/sort/quicksort", (req, res, next) => {
    try {
        const { array, mode, options } = parseSortRequest(req.body);

        const start = process.hrtime.bigint();
        const sorted =
            mode === "recursive"
                ? quickSortRecursive(array, options)
                : quickSortIterative(array, options);
        const end = process.hrtime.bigint();

        const timeMs = Number(end - start) / 1_000_000;

        res.status(200).json({
            algorithm: "QuickSort",
            mode,
            inputSize: array.length,
            timingMs: Number(timeMs.toFixed(4)),
            sorted,
        });
    } catch (error) {
        next(error);
    }
});

app.use((error, _req, res, _next) => {
    const statusCode =
        error instanceof TypeError || error instanceof RangeError ? 400 : 500;

    res.status(statusCode).json({
        error: error.message || "Internal server error.",
    });
});

function startServer(port = process.env.PORT || 3000) {
    return app.listen(port, () => {
        // eslint-disable-next-line no-console
        console.log(`QuickSort API listening on http://localhost:${port}`);
    });
}

if (require.main === module) {
    startServer();
}

module.exports = {
    app,
    startServer,
};
