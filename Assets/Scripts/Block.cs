﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits = 3;
    [SerializeField] Sprite[] hitSprites;

    // state variables
    [SerializeField] int timesHit; //TODO only serialized for debug purposes

    // cached reference
    Level level;

    private void Start() {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks() {
        if (CompareTag("Breakable")) {
            level = FindObjectOfType<Level>();
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (CompareTag("Breakable")) {
            HandleHit();
        }
    }

    private void HandleHit() {
        timesHit++;
        if (timesHit >= maxHits) {
            DestroyBlock();
        }
        else {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite() {
        GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
    }

    private void DestroyBlock() {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX() {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX() {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
