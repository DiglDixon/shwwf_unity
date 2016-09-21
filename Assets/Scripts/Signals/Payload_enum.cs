

public enum Payload{
	NONE, // this has significance as a "null".
	BEGIN_SHOW,
	HAMBURGER,
	CROSS_ROAD,
	DUANJUN,
	RED_BUILDING,
	ARRIVE_304,
	CLOSE_DOOR,
	DORM,
	ELEVATOR_ARRIVES,
	BAI_ROOM,
	FIND_YU_PINGFAN,
	GUANXINTAI,
	FIND_LETTER,
	BLUETOOTH_TEST,
	STOP_BLUETOOTH_TEST,
	FINISH_SETUP,
	QUIET_LOAD_STEP,
	TIME_SYNC_A,
	TIME_SYNC_B,
	EMERGENCY_PAUSE,
	EMERGENCY_UNPAUSE,
	NULL_SIGNAL // this is used as a "no-response" signal. Implemented for triggering a re-fire of the same signal.
}