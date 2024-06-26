// Copyright (c) homuler and The Vignette Authors
// This file is part of MediaPipe.NET.
// MediaPipe.NET is licensed under the MIT License. See LICENSE for details.

using System;

namespace Mediapipe.Net.Framework.Port
{
    public unsafe class StatusOrGpuBuffer : StatusOr<GpuBuffer>
    {
        public StatusOrGpuBuffer(IntPtr ptr) : base(ptr) { }

        protected override void DeleteMpPtr() => UnsafeNativeMethods.mp_StatusOrGpuBuffer__delete(Ptr);

        private Status? status;
        public override Status Status
        {
            get
            {
                if (status == null || status.IsDisposed)
                {
                    UnsafeNativeMethods.mp_StatusOrGpuBuffer__status(MpPtr, out var statusPtr).Assert();

                    GC.KeepAlive(this);
                    status = new Status(statusPtr);
                }
                return status;
            }
        }

        public override bool Ok() => SafeNativeMethods.mp_StatusOrGpuBuffer__ok(MpPtr) > 0;

        public override GpuBuffer Value()
        {
            UnsafeNativeMethods.mp_StatusOrGpuBuffer__value(MpPtr, out var gpuBufferPtr).Assert();
            Dispose();

            return new GpuBuffer(gpuBufferPtr);
        }
    }
}
