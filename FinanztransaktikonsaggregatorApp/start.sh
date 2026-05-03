#!/usr/bin/env bash
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_FILE="$SCRIPT_DIR/FinanztransaktikonsaggregatorApp.csproj"

dotnet run --project "$PROJECT_FILE" -- "$@"
