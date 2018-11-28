<?php
	include 'connection.php';
	$conn = OpenCon();
	$gid = $_GET["Group_id"];
	$password = $_GET["Group_password"];
	$uid = $_GET["User_id"];
	if($result->num_rows != 0)
	{
		$enter = "INSERT INTO chorechomper.group_has_users (id_group, user_id) VALUES ('$gid','$uid')";
		mysqli_query($conn,$enter);
		echo json_encode($gid);
	}
	else
		echo json_encode('-1');
	CloseCon($conn);
	?>