using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    //these will be according to level
    float mSpeed;
    float mScale;
    [SerializeField] float mMinSpeed;
    [SerializeField] float mMaxSpeed;

    //desired shark sizes
    [SerializeField] float mMinScale;
    [SerializeField] float mMaxScale;

    float lifeSpan = 20;
    float second;
    bool gettingExtracted;

    Rigidbody body;

    // Total distance between the markers.
    private float journeyLength;
    // Time when the movement started.
    private float startTime;

    Vector3 point1;
    Vector3 point2;

    // Start is called before the first frame update
    void Start()
    {
        second = 0;
        gettingExtracted = false;

        body = GetComponent<Rigidbody>();

        //random speeds and scale
        mSpeed = Random.Range(mMinSpeed, mMaxSpeed);
        mScale = Random.Range(mMinScale, mMaxScale);
        transform.localScale *= mScale;
    }

    // Update is called once per frame
    void Update()
    {
        LifeSpan();
        GoToExtraction();
    }

    void LifeSpan()
    {
        second += Time.deltaTime; //count 1 second
        if (second >= lifeSpan)
        {
            Destroy(gameObject);
        }
        if (Extractor_Beam.extracting)
        {
            second = 0;
        }
    }

    void GoToExtraction()
    {
        float distance = Vector3.Distance(transform.position, Extractor_Beam.extractor_beam_position);

        if (!Extractor_Beam.extracting || distance >= 10)
        {
            gettingExtracted = false;
            transform.Translate(-Vector3.right * Time.deltaTime * mSpeed, Space.World);
        }

        if (Extractor_Beam.extracting && !gettingExtracted && distance < 10)
        {
            gettingExtracted = true;
            point1 = transform.position;
            point2 = new Vector3(0, Extractor_Beam.extractor_beam_position.y, transform.position.z);

            startTime = Time.time;
            journeyLength = Vector3.Distance(point1, point2);
        }

        if (gettingExtracted)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * mSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            //transform.right is a vector 3
            transform.position = Vector3.Lerp(point1, point2, fractionOfJourney);

            Vector3 direction = (point1 - point2).normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-direction), 1 * Time.deltaTime);

            if (transform.position.y == Extractor_Beam.extractor_beam_position.y)
            {
                if (gameObject.tag == "Resource_Water")
                {
                    WaterStorageRoom.resources_Water += 5;
                }
                if (gameObject.tag == "Resource_Metal")
                {
                    Storage.resources_Metal += 5;
                }

                gettingExtracted = false;

                Destroy(gameObject);
            }
        }
    }
}
