<?php
	include 'connection.php';
	$conn = OpenCon();
	$username = $_GET["Username"];
	$sql = ("SELECT user_id FROM chorechomper.user WHERE Username = '$username'");
	$result = mysqli_query($conn,$sql);
	
	if($result->num_rows == 1)
	{
		$row = mysqli_fetch_assoc($result);
		$uid = $row['user_id'];
		echo json_encode($uid);
	}
	else
		echo json_encode('-1');
	CloseCon($conn);
	?>