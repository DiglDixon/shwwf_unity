
public struct SignalTime{
	public int minute;
	public int second;
	public SignalTime(int minute, int second){
		this.minute = minute;
		this.second = second;
	}
	public string GetPrint(){
		return minute + ":" + second;
	}
}