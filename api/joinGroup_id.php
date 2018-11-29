<?php
	//this files is just like joinGroup.php but the parameter takes in the group_id instead of group_name
	include 'connection.php';
	$conn = OpenCon();

	$gid = $_GET["Group_id"];
	$password = $_GET["Group_password"];
	$uid = $_GET["User_id"];

	if($result->num_rows != 0)
	{
		$enter_into_relational = "INSERT INTO chorechomper.group_has_users (id_group, user_id) VALUES ('$gid','$uid')";
		mysqli_query($conn,$enter_into_relational);
		echo json_encode($gid);
	}
	
	else
		echo json_encode('-1');
	
	CloseCon($conn);
	?>
