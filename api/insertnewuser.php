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
	$default = 'default';
	$combined_key = $Username_ . $default;
	$combined_password = $Username_ . $Password_;
	//echo "$Email_ $FirstName_ $LastName_ $Password_ $Phone_ $Username_";
	$new_user_sql = "INSERT into chorechomper.user (Email,FirstName,LastName,Password,Phone,Username) Values ('$Email_','$FirstName_','$LastName_','$Password_','$Phone_','$Username_')";
	$new_group_sql = "INSERT into chorechomper.group (nameGroup, groupPassword) Values ('$combined_key','$combined_password')";
	$get_uid = "SELECT user_id FROM chorechomper.user WHERE Username = '$Username_'";
	
	if(mysqli_query($conn,$new_user_sql))
	{
	//	echo "insertion successful";
		if(mysqli_query($conn,$new_group_sql))
		{
	//		echo "good";
			$dummyTitle = 'MakeGroup';
			$dummyTime = 'now';
			$dummyPri = '1';
			$u_result = mysqli_query($conn,$get_uid);
			$row_u = mysqli_fetch_assoc($u_result);
			$uid = $row_u['user_id'];
			$get_gid = "SELECT idGroup FROM chorechomper.group WHERE nameGroup = '$combined_key'";
			$g_result = mysqli_query($conn,$get_gid);
			$row_g = mysqli_fetch_assoc($g_result);
			$gid = $row_g['idGroup'];
			$new_group_has_users_sql = "INSERT into chorechomper.group_has_users (id_group, user_id) VALUES ('$gid', '$uid')";
			mysqli_query($conn,$new_group_has_users_sql);
			$a="INSERT INTO chorechomper.chores (chore_title, complete_by_date, priority, assigned_to) VALUES ('Make Group', '0/0/0000', '1', '$uid')";
			mysqli_query($conn,$a);
			$get_chore_id = "SELECT c_ID FROM chorechomper.chores WHERE chore_title = 'Make Group' AND complete_by_date = 'now' AND assigned_to = '$uid' AND priority = '1'";
            $c_result = mysqli_query($conn,$get_chore_id);
            $row_c = mysqli_fetch_assoc($c_result);
			$cid = $row_c['c_ID'];
			$new_user_chore = "INSERT into user_has_chores (user_id, chore_id) Values ('$uid', '$cid')";
			mysqli_query($conn,$new_user_chore);
			echo json_encode($uid);
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
