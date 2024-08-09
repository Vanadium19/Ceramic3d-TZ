using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Utils;

[BurstCompile]
public struct FindOffsetsJob : IJobParallelFor
{
    private Matrix4x4 _modelMatrix;
    private NativeArray<Matrix4x4> _modelMatrices;
    private NativeArray<Matrix4x4> _spaceMatrices;
    private NativeArray<Matrix4x4> _offsets;
    private NativeArray<bool> _offsetsFlags;

    public FindOffsetsJob(
        NativeArray<Matrix4x4> modelMatrices,
        NativeArray<Matrix4x4> spaceMatrices,
        NativeArray<Matrix4x4> offsets,
        NativeArray<bool> matchFlags)
    {
        _modelMatrices = modelMatrices;
        _modelMatrix = modelMatrices[0];
        _spaceMatrices = spaceMatrices;
        _offsets = offsets;
        _offsetsFlags = matchFlags;
    }

    public void Execute(int index)
    {
        Matrix4x4 offset = math.mul(_spaceMatrices[index], _modelMatrix.inverse);

        if (IsMatchWithOffset(_modelMatrices, _spaceMatrices, offset))
        {
            _offsets[index] = offset;
            _offsetsFlags[index] = true;
        }
    }

    private bool IsMatchWithOffset(
        NativeArray<Matrix4x4> modelMatrices,
        NativeArray<Matrix4x4> spaceMatrices,
        Matrix4x4 offset)
    {
        bool matchFound = false;
        Matrix4x4 transformedMatrix;

        foreach (var modelMatrix in modelMatrices)
        {
            transformedMatrix = math.mul(offset, modelMatrix);

            foreach (var spaceMatrix in spaceMatrices)
            {
                matchFound = transformedMatrix.IsEqual(spaceMatrix);

                if (matchFound)
                    break;
            }

            if (!matchFound)
                return false;
        }

        return true;
    }
}