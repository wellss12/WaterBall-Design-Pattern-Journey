var waterBall = new WaterBall();
var fireBall = new FireBall();
var pewDiePieChannel = new YoutubeChannel("PewDiePie");
var waterBallChannel = new YoutubeChannel("水球軟體學院");

waterBallChannel.Subscribe(waterBall);
pewDiePieChannel.Subscribe(waterBall);
waterBallChannel.Subscribe(fireBall);
pewDiePieChannel.Subscribe(fireBall);

var video1 = new Video("C1M1S2", "這個世界正是物件導向的呢!", new TimeSpan(0,0,4,0));
var video2 = new Video("Hello guys", "Clickbait", new TimeSpan(0,0,0,30));
var video3 = new Video("C1M1S3", "物件 vs. 類別", new TimeSpan(0,0,1,0));
var video4 = new Video("Minecraft", "Let's play Minecraft", new TimeSpan(0,0,30,0));

waterBallChannel.Upload(video1);
pewDiePieChannel.Upload(video2);
waterBallChannel.Upload(video3);
pewDiePieChannel.Upload(video4);
Console.ReadLine();
