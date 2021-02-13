using System;

public enum Direction {
    DOWN,
    RIGHT,
    UP,
    LEFT
}

public static class DirectionExtension {
    public static int GetIntValue(this Direction tuneType) {
        switch (tuneType) {
            case Direction.DOWN:
                return 1;
            case Direction.UP:
                return 3;
            case Direction.RIGHT:
                return 2;
            case Direction.LEFT:
                return 4;
            default:
                throw new ArgumentOutOfRangeException(nameof(tuneType), tuneType, null);
        }
    }

    public static Direction Rotate(this Direction tuneType, int rotate) {
        switch (tuneType) {
            case Direction.DOWN:
                if (rotate == 0) {
                    return Direction.DOWN;
                }
                else if (rotate == 1) {
                    return Direction.RIGHT;
                }
                else if (rotate == 2) {
                    return Direction.UP;
                }
                else if (rotate == 3) {
                    return Direction.LEFT;
                }

                throw new Exception();
            case Direction.UP:

                if (rotate == 0) {
                    return Direction.UP;
                }
                else if (rotate == 1) {
                    return Direction.LEFT;
                }
                else if (rotate == 2) {
                    return Direction.DOWN;
                }
                else if (rotate == 3) {
                    return Direction.RIGHT;
                }

                throw new Exception();
            case Direction.RIGHT:

                if (rotate == 0) {
                    return Direction.RIGHT;
                }
                else if (rotate == 1) {
                    return Direction.UP;
                }
                else if (rotate == 2) {
                    return Direction.LEFT;
                }
                else if (rotate == 3) {
                    return Direction.DOWN;
                }

                throw new Exception();
            case Direction.LEFT:

                if (rotate == 0) {
                    return Direction.LEFT;
                }
                else if (rotate == 1) {
                    return Direction.DOWN;
                }
                else if (rotate == 2) {
                    return Direction.RIGHT;
                }
                else if (rotate == 3) {
                    return Direction.UP;
                }

                throw new Exception();
            default:
                throw new ArgumentOutOfRangeException(nameof(tuneType), tuneType, null);
        }
    }
}