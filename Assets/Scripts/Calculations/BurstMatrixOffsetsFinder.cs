using System.Collections.Generic;
using Calculations.Interfaces;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

internal class BurstMatrixOffsetsFinder : IMatrixOffsetsFinder
{
    private readonly int _innerloopBatchCount = 64;

    private NativeArray<Matrix4x4> _modelMatrices;
    private NativeArray<Matrix4x4> _spaceMatrices;
    private NativeArray<Matrix4x4> _offsets;
    private NativeArray<bool> _offsetsFlags;

    public List<Matrix4x4> FindOffsets(Matrix4x4[] modelMatrices, Matrix4x4[] spaceMatrices)
    {
        _modelMatrices = new NativeArray<Matrix4x4>(modelMatrices, Allocator.TempJob);
        _spaceMatrices = new NativeArray<Matrix4x4>(spaceMatrices, Allocator.TempJob);
        _offsets = new NativeArray<Matrix4x4>(spaceMatrices.Length, Allocator.TempJob);
        _offsetsFlags = new NativeArray<bool>(spaceMatrices.Length, Allocator.TempJob);

        var job = new FindOffsetsJob(_modelMatrices, _spaceMatrices, _offsets, _offsetsFlags);

        JobHandle jobHandle = job.Schedule(spaceMatrices.Length, _innerloopBatchCount);
        jobHandle.Complete();

        return GetResult();
    }

    private List<Matrix4x4> GetResult()
    {
        List<Matrix4x4> result = new List<Matrix4x4>();

        for (int i = 0; i < _offsets.Length; i++)
        {
            if (_offsetsFlags[i])
            {
                result.Add(_offsets[i]);
            }
        }

        Dispose();

        return result;
    }

    private void Dispose()
    {
        _modelMatrices.Dispose();
        _spaceMatrices.Dispose();
        _offsets.Dispose();
        _offsetsFlags.Dispose();
    }
}