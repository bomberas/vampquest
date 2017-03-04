/******************************************************************************
 * Spine Runtimes Software License v2.5
 *
 * Copyright (c) 2013-2016, Esoteric Software
 * All rights reserved.
 *
 * You are granted a perpetual, non-exclusive, non-sublicensable, and
 * non-transferable license to use, install, execute, and perform the Spine
 * Runtimes software and derivative works solely for personal or internal
 * use. Without the written permission of Esoteric Software (see Section 2 of
 * the Spine Software License Agreement), you may not (a) modify, translate,
 * adapt, or develop new applications using the Spine Runtimes or otherwise
 * create derivative works or improvements of the Spine Runtimes or (b) remove,
 * delete, alter, or obscure any trademarks or any copyright, trademark, patent,
 * or other intellectual property or proprietary rights notices on or in the
 * Software, including any copy thereof. Redistributions in binary or source
 * form must include this license and terms.
 *
 * THIS SOFTWARE IS PROVIDED BY ESOTERIC SOFTWARE "AS IS" AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
 * EVENT SHALL ESOTERIC SOFTWARE BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
 * PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES, BUSINESS INTERRUPTION, OR LOSS OF
 * USE, DATA, OR PROFITS) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

// Contributed by: Mitch Thompson

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainedCamera : MonoBehaviour {
	public Transform target;
	public Vector3 offset;
	public Vector3 min;
	public Vector3 max;
	public float smoothing = 5f;

    private void Start()
    {
        //currentLevel = GameManagerL1.Instance.currentLevel;
    }


    // Update is called once per frame
    void LateUpdate () {
		Vector3 goalPoint = target.position + offset;
		goalPoint.x = Mathf.Clamp(goalPoint.x, min.x, max.x);
		goalPoint.y = Mathf.Clamp(goalPoint.y, min.y, max.y);
		goalPoint.z = Mathf.Clamp(goalPoint.z, min.z, max.z);

		transform.position = Vector3.Lerp(transform.position, goalPoint, smoothing * Time.deltaTime);


        //-------------
        //CODE FOR MOVING CLOUDS.   
        /*In alternative: you can use Update here (now it's LateUpdate this function) and activate the two scripts 
         * attached to the clouds (that work in LateUpdate). 
         * The reason that I put this code here (or in lateUpdate in the clouds scripts) is that the clouds (this code) should 
         * follow the camera only AFTER the camera has moved.*/
       

        //-------------
        /*CODE FOR ZOOMING IN/OUT WHEN PLAYER MOVES (optional)
         * */ 
         switch (1)
        {
            
            case 1:
                {
                    if (target.position.x < 45)
                    {
                        Camera.main.GetComponent<ConstrainedCamera>().offset.z = -9f;
                    }else if (target.position.x >= 45  && target.position.x <= 163)
                    {

                        if (target.position.y <= 25)
                        {
                            Camera.main.GetComponent<ConstrainedCamera>().offset.z = -14f;
                        }
                        else
                        {
                            Camera.main.GetComponent<ConstrainedCamera>().offset.z = -9f;
                        }

                    }else if (target.position.x >= 163 && target.position.x <= 338)
                    {
                        Camera.main.GetComponent<ConstrainedCamera>().offset.z = -10f;

                    }else if (target.position.x > 338 && target.position.x < 366)
                    {
                        Camera.main.GetComponent<ConstrainedCamera>().offset.z = -15f;
                    }
                    else if (target.position.x >= 366 && target.position.x <= 460)
                    {
                        Camera.main.GetComponent<ConstrainedCamera>().offset.z = -12f;
                    }
                    else if (target.position.x > 460 && target.position.x <= 488)
                    {
                        Camera.main.GetComponent<ConstrainedCamera>().offset.z = -9f;
                    }
                    else if (target.position.x > 488)
                    {
                        Camera.main.GetComponent<ConstrainedCamera>().offset.z = -9.8f;
                    }
                    break;
                }
        }
        


    }


}	
