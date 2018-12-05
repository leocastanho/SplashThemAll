using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : EnemyController {

    private int rand;

	void Update () {
        rand = Random.Range(0,7);

        if (timeBtwShots <= 0)
        {
            if (rand == 0)
            {
                //spawn projectile on enemys position with no rotation
                Instantiate(projectile, transform.position, Quaternion.identity);
                //if we dont use this command, the enemy will shot 60 shots per second! -> if we dont use it. It can make a cool effect of a whip, but we wont use on this project                    
                timeBtwShots = startTimeBtwShots;
            }
            if (rand == 1)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = 3.5f;
            }
            if (rand == 2)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = 0.05f;
            }
            if (rand == 3)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = 0.05f;
            }
            if (rand == 4)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = 0.05f;
            }
            if (rand == 5)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = 0.05f;
            }
            if (rand == 6)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBtwShots = 0.05f;
            }

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }
}
