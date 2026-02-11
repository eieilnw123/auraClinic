"use client";
import { useState, useEffect } from "react";

export default function PatientPage() {
  const [patients, setPatients] = useState([]);
  const [loading, setLoading] = useState(false);
  const [form, setForm] = useState({ firstName: "", lastName: "", phoneNumber: "", primaryBranchId: "3fa85f64-5717-4562-b3fc-2c963f66afa6" });

  const headers = {
    "X-Tenant-Id": "550e8400-e29b-41d4-a716-446655440000",
    "X-User-Role": "Admin",
    "Content-Type": "application/json"
  };

  const fetchPatients = async () => {
    setLoading(true);
    try {
      const res = await fetch("http://localhost:5268/api/Patient", { headers });
      if (res.ok) setPatients(await res.json());
    } catch (err) {
      console.error("Fetch error:", err);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const res = await fetch("http://localhost:5268/api/Patient", {
      method: "POST",
      headers,
      body: JSON.stringify(form)
    });

    if (res.ok) {
      setForm({ ...form, firstName: "", lastName: "", phoneNumber: "" });
      fetchPatients();
    } else {
      const errorMsg = await res.text();
      alert(`Error: ${errorMsg}`);
    }
  };

  useEffect(() => { fetchPatients(); }, []);

  return (
    <div className="min-h-screen bg-slate-50 py-12 px-4 sm:px-6 lg:px-8 font-sans">
      <div className="max-w-5xl mx-auto">

        {/* Header Section */}
        <div className="mb-10 flex items-center justify-between">
          <div>
            <h1 className="text-4xl font-extrabold text-slate-900 tracking-tight">Clinic Management</h1>
            <p className="mt-2 text-lg text-slate-600">Patient registration and database (Section A & B)</p>
          </div>
          <div className="bg-blue-100 text-blue-700 px-4 py-2 rounded-full text-sm font-semibold shadow-sm border border-blue-200">
            Tenant: 550e8400...
          </div>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">

          {/* Form Section */}
          <div className="lg:col-span-1">
            <div className="bg-white p-8 rounded-2xl shadow-xl border border-slate-200 sticky top-8">
              <h2 className="text-xl font-bold text-slate-800 mb-6 flex items-center">
                <span className="bg-blue-600 w-2 h-6 rounded-full mr-3"></span>
                Add New Patient
              </h2>
              <form onSubmit={handleSubmit} className="space-y-5">
                <div>
                  <label className="block text-sm font-medium text-slate-700 mb-1">First Name</label>
                  <input className="w-full border-slate-200 border p-3 rounded-xl focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all shadow-sm" value={form.firstName} placeholder="John" onChange={e => setForm({ ...form, firstName: e.target.value })} required />
                </div>
                <div>
                  <label className="block text-sm font-medium text-slate-700 mb-1">Last Name</label>
                  <input className="w-full border-slate-200 border p-3 rounded-xl focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all shadow-sm" value={form.lastName} placeholder="Doe" onChange={e => setForm({ ...form, lastName: e.target.value })} required />
                </div>
                <div>
                  <label className="block text-sm font-medium text-slate-700 mb-1">Phone Number</label>
                  <input className="w-full border-slate-200 border p-3 rounded-xl focus:ring-2 focus:ring-blue-500 focus:border-transparent outline-none transition-all shadow-sm" value={form.phoneNumber} placeholder="081-234-5678" onChange={e => setForm({ ...form, phoneNumber: e.target.value })} required />
                </div>
                <button type="submit" className="w-full bg-blue-600 text-white font-bold py-4 rounded-xl shadow-lg hover:bg-blue-700 hover:-translate-y-0.5 transition-all active:scale-95 mt-4">
                  Register Patient
                </button>
              </form>
            </div>
          </div>

          {/* Table Section */}
          <div className="lg:col-span-2">
            <div className="bg-white rounded-2xl shadow-xl border border-slate-200 overflow-hidden">
              <div className="p-6 border-b border-slate-100 flex justify-between items-center bg-white">
                <h2 className="text-xl font-bold text-slate-800 tracking-tight">Patient Directory</h2>
                <button onClick={fetchPatients} className="text-blue-600 hover:bg-blue-50 p-2 rounded-lg transition-colors">
                  {loading ? "Updating..." : "Refresh List"}
                </button>
              </div>
              <div className="overflow-x-auto">
                <table className="w-full text-left">
                  <thead>
                    <tr className="bg-slate-50">
                      <th className="p-5 text-sm font-bold text-slate-600 uppercase tracking-wider border-b">Full Name</th>
                      <th className="p-5 text-sm font-bold text-slate-600 uppercase tracking-wider border-b">Contact</th>
                      <th className="p-5 text-sm font-bold text-slate-600 uppercase tracking-wider border-b">Status</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-slate-100">
                    {patients.length > 0 ? patients.map((p: any) => (
                      <tr key={p.id} className="hover:bg-slate-50 transition-colors">
                        <td className="p-5">
                          <div className="font-semibold text-slate-800">{p.firstName} {p.lastName}</div>
                          <div className="text-xs text-slate-400 font-mono mt-1">UID: {p.id.substring(0, 8)}...</div>
                        </td>
                        <td className="p-5">
                          <span className="text-slate-600 bg-slate-100 px-3 py-1 rounded-md text-sm">{p.phoneNumber}</span>
                        </td>
                        <td className="p-5">
                          <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-green-100 text-green-800">
                            Verified
                          </span>
                        </td>
                      </tr>
                    )) : (
                      <tr>
                        <td colSpan={3} className="p-10 text-center text-slate-400">
                          No patients found for this tenant.
                        </td>
                      </tr>
                    )}
                  </tbody>
                </table>
              </div>
            </div>
          </div>

        </div>
      </div>
    </div>
  );
}