<?php
	include 'connection.php';
	$conn = OpenCon();
	//if($conn === false)
	//{
	//	die("error" . mysqli_connect_error());
	//}
	$FirstName_ = $_GET["FirstName"];
	$Email_ = $_GET["Email"];
	$LastName_ = $_GET["LastName"];
	$Password_ = $_GET["Password"];
	$Phone_ = $_GET["Phone"];
	$Username_ = $_GET["Username"];
	$combined_key = $Username_ . $Password_;
	//echo "$Email_ $FirstName_ $LastName_ $Password_ $Phone_ $Username_";
	$new_user_sql = "INSERT into chorechomper.user (Email,FirstName,LastName,Password,Phone,Username) Values ('$Email_','$FirstName_','$LastName_','$Password_','$Phone_','$Username_')";
	$new_group_sql = "INSERT into chorechomper.group (nameGroup, groupPassword) Values ('$combined_key','$combined_key')";
	$get_uid = "SELECT user_id FROM chorechomper.user WHERE Username = '$Username_'";
	if(mysqli_query($conn,$new_user_sql))
	{
	//	echo "insertion successful";
		if(mysqli_query($conn,$new_group_sql))
		{
	//		echo "good";
			$u_result = mysqli_query($conn,$get_uid);
			$row_u = mysqli_fetch_assoc($u_result);
			$uid = $row_u['user_id'];
			$get_gid = "SELECT idGroup FROM chorechomper.group WHERE nameGroup = '$combined_key'";
			$g_result = mysqli_query($conn,$get_gid);
			$row_g = mysqli_fetch_assoc($g_result);
			$gid = $row_g['idGroup'];
			$new_group_has_users_sql = "INSERT into chorechomper.group_has_users (id_group, user_id) VALUES ('$gid', '$uid')";
			mysqli_query($conn,$new_group_has_users_sql);
			//if(mysqli_query($conn,$new_group_has_users_sql))
			//{
	//			echo "success";
			//}
		}
	}
	//else
	//{
	//	echo "error failed insertion";
	//}
	
	CloseCon($conn);
?>
