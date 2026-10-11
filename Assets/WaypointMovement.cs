using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    [Header("移動速度")]
    public float moveSpeed = 3f;

    [Header("路徑節點陣列 (往前走 -> 上跳 -> 下跳到終點)")]
    // 作業規定：使用 Vector 與 陣列 (Array)
    public Vector2[] waypoints;

    private int currentWaypointIndex = 0;

    void Start()
    {
        // 若 Inspector 未手動指派，給予預設路徑座標
        if (waypoints == null || waypoints.Length == 0)
        {
            Vector2 startPos = transform.position;

            // 宣告並建立 Vector2 陣列
            waypoints = new Vector2[3];
            waypoints[0] = startPos + new Vector2(3f, 0f);        // 1. 往前走
            waypoints[1] = waypoints[0] + new Vector2(2f, 2.5f);  // 2. 上跳
            waypoints[2] = waypoints[1] + new Vector2(2f, -2.5f); // 3. 下跳到終點
        }
    }

    void Update()
    {
        // 到達終點後停止
        if (currentWaypointIndex >= waypoints.Length) return;

        // 取得目前的 Vector 目標點
        Vector2 targetPosition = waypoints[currentWaypointIndex];

        // 使用 Vector2.MoveTowards 移動
        transform.position = Vector2.MoveTowards(
            transform.position, 
            targetPosition, 
            moveSpeed * Time.deltaTime
        );

        // 判定是否到達當前目標點
        if (Vector2.Distance(transform.position, targetPosition) < 0.05f)
        {
            currentWaypointIndex++;
        }
    }
}
