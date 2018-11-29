<?php //returns TaskList
// required headers
header("Access-Control-Allow-Origin: *");
header("Access-Control-Allow-Headers: access");
header("Access-Control-Allow-Methods: GET");
header("Access-Control-Allow-Credentials: true");
header("Content-Type: application/json; charset=UTF-8");
 
// include database and object files
include_once '../config/database.php';
include_once '../objects/chore.php';
 
// get database connection
$database = new Database();
$db = $database->getConnection();
 
// initialize object
$chore = new Chore($db);
 
// set ID property of record to read
$chore->u_ID = isset($_GET['u_ID']) ? $_GET['u_ID'] : die();

// query products
$stmt = $chore->readGroups();
$num = $stmt->rowCount();
 
// check if more than 0 record found
if($num>0){
 
    // products array
    $chore_arr=array();
    $chore_arr["groups"]=array();
    // retrieve our table contents
    while ($row = $stmt->fetch(PDO::FETCH_ASSOC)){
        // extract row
        // this will make $row['name'] to
        // just $name only
        extract($row);
 
        $chore_item=array(
            "id_group" => $idGroup,
			"group_name" => $nameGroup
			
        );
 
        array_push($chore_arr["groups"], $chore_item);
    }
 
    // set response code - 200 OK
    http_response_code(200);
 
    // show products data in json format
    echo json_encode($chore_arr);
}
 else{
 
    // set response code - 404 Not found
    http_response_code(404);
 
    // tell the user no groups found
    echo json_encode($chore_arr);
}
?>